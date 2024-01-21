namespace Demo2.Common
{
    public abstract class Payload
    {
        protected Payload(IReadOnlyList<ErrorResult>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<ErrorResult>? Errors { get; }
    }

    public class ErrorResult
    {
        public ErrorResult(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public string Code { get; }
    }
}
