using Microsoft.Extensions.DependencyInjection;

using APSS.Application.App;
using APSS.Domain.Services;
using APSS.Infrastructure.Services;
using APSS.Tests.Infrastructure.Repositories.EntityFramework.Util;
using APSS.Tests.Utils;

namespace APSS.Tests.Application.App;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(_ => TestUnitOfWork.Create());

        services.AddTransient<ICryptoHashService, Argon2iCryptoHashService>();
        services.AddTransient<IRandomGeneratorService, SimpleRandomGeneratorService>();
        services.AddTransient<IAccountsService, AccountsService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<ILogsService, DatabaseLogsService>();
        services.AddTransient<IPermissionsService, PermissionsService>();
        services.AddTransient<IPopulationService, PopulationService>();
        services.AddTransient<ISurveysService, SurveysService>();
        services.AddTransient<ILandService, LandService>();
        services.AddTransient<IAnimalService, AnimalService>();
    }
}