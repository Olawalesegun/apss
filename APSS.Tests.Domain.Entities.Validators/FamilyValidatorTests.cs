using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class FamilyValidatorTests
{
    #region Private fields

    private readonly FamilyValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateFamilyShouldSucceed()
    {
        var family = new Family
        {
            Name = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(family).IsValid);
    }

    [TestMethod]
    public void ValidateFamilyShouldFail()
    {
        var family = new Family
        {
            Name = "",
        };

        var result = _validator.TestValidate(family);

        result.ShouldHaveValidationErrorFor(a => a.Name);
    }

    #endregion Tests
}