using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class MultipleChoiceAnswerItemValidatorTests
{
    #region Private fields

    private readonly MultipleChoiceAnswerItemValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateMultipleChoiceAnswerItemShouldSucceed()
    {
        var item = new MultipleChoiceAnswerItem
        {
            Value = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(item).IsValid);
    }

    [TestMethod]
    public void ValidateMultipleChoiceAnswerItemShouldFail()
    {
        var item = new MultipleChoiceAnswerItem
        {
            Value = "",
        };

        var result = _validator.TestValidate(item);

        result.ShouldHaveValidationErrorFor(l => l.Value);
    }

    #endregion Tests
}