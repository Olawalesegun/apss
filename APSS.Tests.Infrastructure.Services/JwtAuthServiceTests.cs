using APSS.Domain.Repositories;
using APSS.Domain.Services;

namespace APSS.Tests.Infrastructure.Services;

public sealed class JwtAuthServiceTests
{
    #region Fields

    private readonly IAuthService _authSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    public JwtAuthServiceTests(IUnitOfWork uow, IAuthService authSvc)
    {
        _uow = uow;
        _authSvc = authSvc;
    }

    #endregion Public Constructors
}