namespace GaussPlatform.Exceptions;

public class ApplicationStartException:Exception
{
    public ApplicationStartException() {}
    public ApplicationStartException(string message) : base(message) {}
    public ApplicationStartException(string message, Exception inner) : base(message, inner) {}
}