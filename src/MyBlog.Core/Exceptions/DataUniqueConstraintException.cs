namespace MyBlog.Core.Exceptions
{
    public class DataUniqueConstraintException : Exception
    {
        public DataUniqueConstraintException()
        {
        }

        public DataUniqueConstraintException(string? message) : base(message)
        {
        }

        public DataUniqueConstraintException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}