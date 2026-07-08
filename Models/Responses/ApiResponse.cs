namespace ApiMantenimiento.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set;  }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Fail(string message = "")=>
            new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };

        public static ApiResponse<T> SuccessResult(T data, string mensaje = "¡¡Operacion Exitosa!!") =>
            new ApiResponse<T>
            {
                Success = true,
                Message = mensaje,
                Data = data
            };
     
    }
}
