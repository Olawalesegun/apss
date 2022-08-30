using APSS.Domain.Entities;

namespace APSS.Domain.Services;

public interface ISetupService
{
    /// <summary>
    /// Asynchronously sets up the system for the first time
    /// </summary>
    /// <param name="holderName">The name of the root account holder</param>
    /// <param name="password">The password of the root account</param>
    /// <returns>The created root account</returns>
    /// <exception cref="SystemAlreadySetupException">
    /// Thrown when the system is already setup or in invalid state
    /// </exception>
    Task<Account> SetupAsync(string holderName, string password);

    /// <summary>
    /// Asynchronously checks whether the system has already been set up or not
    /// </summary>
    /// <returns>True if the system has not already been setup, false otherwise</returns>
    Task<bool> CanSetupAsync();
}