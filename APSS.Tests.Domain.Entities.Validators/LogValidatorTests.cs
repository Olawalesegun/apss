using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class LogValidatorTests
{
    #region Private fields

    private readonly LogValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateMessageShouldSucceed()
    {
        var log = new Log
        {
            Message = _rndSvc.NextString(0xff)
        };

        Assert.IsTrue(_validator.Validate(log).IsValid);
    }

    [TestMethod]
    public void ValidateMessageShouldFail()
    {
        var log = new Log { Message = "" };

        var result = _validator.TestValidate(log);

        result.ShouldHaveValidationErrorFor(l => l.Message);
    }

    #endregion Tests
}