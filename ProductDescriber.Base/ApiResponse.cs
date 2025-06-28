namespace ProductDescriber.Base;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data = default, string? message = null, int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static ApiResponse<T> FailResponse(string? message = null, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Data = default
        };
    }
}
