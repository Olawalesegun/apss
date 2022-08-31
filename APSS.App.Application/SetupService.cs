using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

public sealed class SetupService : ISetupService
{
    #region Fields

    private readonly IUnitOfWork _uow;
    private readonly IAccountsService _accountsSvc;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit-of-work of the application</param>
    public SetupService(IUnitOfWork uow, IAccountsService accountsSvc)
    {
        _uow = uow;
        _accountsSvc = accountsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<bool> CanSetupAsync()
        => await _uow.Users.Query().CountAsync() == 0 && await _uow.Accounts.Query().CountAsync() == 0;

    /// <inheritdoc/>
    public async Task<Account> SetupAsync(string holderName, string password)
    {
        if (!await CanSetupAsync())
            throw new SystemAlreadySetupException();

        var rootUser = new User
        {
            Name = "Root User",
            AccessLevel = AccessLevel.Root,
            UserStatus = UserStatus.Active
        };

        await using var tx = await _uow.BeginTransactionAsync();

        _uow.Users.Add(rootUser);

        return await _accountsSvc.CreateUncheckedAsync(
            rootUser,
            holderName,
            password,
            PermissionType.Full,
            tx);
    }

    #endregion Public Methods
}