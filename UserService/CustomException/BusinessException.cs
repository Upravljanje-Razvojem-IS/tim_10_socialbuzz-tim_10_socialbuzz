namespace UserService.CustomException
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public BusinessException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
