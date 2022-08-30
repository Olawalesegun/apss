using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class LogTagValidatorTests
{
    #region Private fields

    private readonly LogTagValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateLogTagShouldSucceed()
    {
        var logTag = new LogTag
        {
            Value = _rndSvc.NextString(0xff)
        };

        Assert.IsTrue(_validator.Validate(logTag).IsValid);
    }

    [TestMethod]
    public void ValidateLogTagShouldFail()
    {
        var logTag = new LogTag { Value = "" };

        var result = _validator.TestValidate(logTag);

        result.ShouldHaveValidationErrorFor(l => l.Value);
    }

    #endregion Tests
}