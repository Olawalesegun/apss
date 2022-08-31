namespace APSS.Domain.Services.Exceptions;

/// <summary>
/// An exception to be thrown if an attempt to setup an already set up system
/// </summary>
public sealed class SystemAlreadySetupException : Exception
{
    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public SystemAlreadySetupException()
        : base("system has already been setup or in an invalid state")
    {
    }

    #endregion Public Constructors
}