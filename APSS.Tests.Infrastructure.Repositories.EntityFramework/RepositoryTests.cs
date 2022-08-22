using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Infrastructure.Repositores.EntityFramework;
using APSS.Tests.Domain.Entities.Validators;
using APSS.Tests.Infrastructure.Repositories.EntityFramework.Util;
using APSS.Tests.Utils;

namespace APSS.Tests.Infrastructure.Repositories.EntityFramework;

[TestClass]
public class RepositoryTests
{
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    [TestMethod]
    public void AddShouldSucceed()
    {
        using var ctx = TestDbContext.Create();
        using var uow = new ApssUnitOfWork(ctx);

        var log = ValidEntitiesFactory.CreateValidLog();

        uow.Logs.Add(log);

        Assert.AreEqual(1, uow.CommitAsync().Result);
        Assert.IsTrue(ctx.Logs.Any(l => l.Id == log.Id));
    }

    [TestMethod]
    public void UpdateShouldSucceed()
    {
        using var ctx = TestDbContext.Create();
        using var uow = new ApssUnitOfWork(ctx);

        var log = ValidEntitiesFactory.CreateValidLog();

        uow.Logs.Add(log);

        Assert.AreEqual(1, uow.CommitAsync().Result);
        Assert.IsTrue(ctx.Logs.Any(l => l.Id == log.Id));

        log.Message = _rndSvc.NextString(0xff);

        uow.Logs.Update(log);

        Assert.AreEqual(1, uow.CommitAsync().Result);
        Assert.IsTrue(ctx.Logs.Any(l => l.Id == log.Id && l.Message == log.Message));
    }

    [TestMethod]
    public void DeleteShouldSucceed()
    {
        using var ctx = TestDbContext.Create();
        using var uow = new ApssUnitOfWork(ctx);

        var log = ValidEntitiesFactory.CreateValidLog();

        uow.Logs.Add(log);

        Assert.AreEqual(1, uow.CommitAsync().Result);
        Assert.IsTrue(ctx.Logs.Any(l => l.Id == log.Id));

        uow.Logs.Remove(log);

        Assert.AreEqual(1, uow.CommitAsync().Result);
        Assert.IsFalse(ctx.Logs.Any(l => l.Id == log.Id));
    }
}