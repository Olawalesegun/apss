namespace APSS.Domain.Services.Exceptions;

public sealed class InvalidAccountIdOrPasswordException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public InvalidAccountIdOrPasswordException()
        : base($"invalid account id or password")
    {
    }
}