using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    [DbContext(typeof(ApiMantenimiento.Data.Context.MantenimientoDbContext))]
    [Migration("20260609000000_AddValeTrigger")]
    public partial class AddValeTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE TRIGGER TR_Man_OTMaterial_AfterInsert
ON Man_OTMaterial
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @id_ot INT;
    DECLARE @creado_por NVARCHAR(150);
    DECLARE @observaciones NVARCHAR(500);
    DECLARE @tipo_ot NVARCHAR(12);
    DECLARE @cod_ot NVARCHAR(20);

    -- Cursor to iterate through distinct id_ot in inserted
    DECLARE ot_cursor CURSOR FOR
    SELECT DISTINCT i.id_ot
    FROM inserted i;

    OPEN ot_cursor;
    FETCH NEXT FROM ot_cursor INTO @id_ot;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Get OT information to populate Vale
        SELECT @creado_por = creado_por, @observaciones = observaciones, @tipo_ot = tipo_ot, @cod_ot = cod_ot
        FROM Man_OrdenTrabajo
        WHERE id_ot = @id_ot;

        -- Only generate Vale if it's correctiva or preventiva
        IF @tipo_ot IN ('PREVENTIVA', 'CORRECTIVA')
        BEGIN
            -- Check if Vale already exists for this OT
            IF NOT EXISTS (SELECT 1 FROM Alm_Vale WHERE id_ot = @id_ot)
            BEGIN
                -- Generate cod_vale: ""VAL-XXXXX""
                DECLARE @next_num INT = 1;
                DECLARE @last_cod_vale NVARCHAR(50);
                
                -- Get the last generated code
                SELECT TOP 1 @last_cod_vale = cod_vale
                FROM Alm_Vale
                WHERE cod_vale LIKE 'VAL-%'
                ORDER BY id_vale DESC;

                IF @last_cod_vale IS NOT NULL
                BEGIN
                    -- Extract the number from VAL-XXXXX (starts at index 5)
                    SET @next_num = CAST(SUBSTRING(@last_cod_vale, 5, LEN(@last_cod_vale) - 4) AS INT) + 1;
                END

                DECLARE @cod_vale NVARCHAR(50) = 'VAL-' + RIGHT('00000' + CAST(@next_num AS VARCHAR(10)), 5);

                -- Insert new Vale
                INSERT INTO Alm_Vale (cod_vale, id_ot, estado, fecha_creacion, solicitado_por, observaciones)
                VALUES (@cod_vale, @id_ot, 'PENDIENTE', GETDATE(), ISNULL(@creado_por, 'Sistema'), 'Generado automáticamente desde OT ' + ISNULL(@cod_ot, ''));
            END

            -- Get the id_vale (either newly created or pre-existing)
            DECLARE @id_vale INT;
            SELECT @id_vale = id_vale FROM Alm_Vale WHERE id_ot = @id_ot;

            IF @id_vale IS NOT NULL
            BEGIN
                -- Insert ValeMaterial rows for materials in this OT that don't have ValeMaterial entries yet
                INSERT INTO Alm_ValeMaterial (id_vale, id_material, cantidad_solicitada)
                SELECT @id_vale, i.id_material_ref, i.cantidad_requerida
                FROM inserted i
                WHERE i.id_ot = @id_ot
                  AND i.id_material_ref IS NOT NULL
                  AND NOT EXISTS (
                      SELECT 1 
                      FROM Alm_ValeMaterial 
                      WHERE id_vale = @id_vale 
                        AND id_material = i.id_material_ref
                  );
            END
        END

        FETCH NEXT FROM ot_cursor INTO @id_ot;
    END

    CLOSE ot_cursor;
    DEALLOCATE ot_cursor;
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TR_Man_OTMaterial_AfterInsert;");
        }
    }
}
