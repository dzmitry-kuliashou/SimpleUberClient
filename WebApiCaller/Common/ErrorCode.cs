namespace WebApiCaller.Common
{
    public class ErrorCode
    {
        public int Code { get; private set; }

        public string Message { get; private set; }

        public ErrorCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
