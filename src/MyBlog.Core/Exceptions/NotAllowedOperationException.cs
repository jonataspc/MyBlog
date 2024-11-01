namespace MyBlog.Core.Exceptions
{
    public class NotAllowedOperationException : Exception
    {
        public NotAllowedOperationException()
        {
        }

        public NotAllowedOperationException(string? message) : base(message)
        {
        }

        public NotAllowedOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}