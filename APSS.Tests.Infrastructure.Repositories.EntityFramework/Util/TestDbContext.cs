using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using APSS.Infrastructure.Repositores.EntityFramework;
using APSS.Tests.Utils;

namespace APSS.Tests.Infrastructure.Repositories.EntityFramework.Util;

public static class TestDbContext
{
    /// <summary>
    /// Creates an in-memory database context
    /// </summary>
    /// <returns></returns>
    public static ApssDbContext Create()
    {
        var dbNameSuffix = new SimpleRandomGeneratorService()
            .NextString(16, APSS.Domain.Services.RandomStringOptions.AlphaNumeric);

        var options = new DbContextOptionsBuilder<ApssDbContext>()
            .UseInMemoryDatabase($"test_apss_db_{dbNameSuffix}")
            .ConfigureWarnings((o) => o.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new ApssDbContext(options);
    }
}