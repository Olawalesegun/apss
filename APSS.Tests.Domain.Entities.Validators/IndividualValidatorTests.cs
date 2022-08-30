using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class IndividualValidatorTests
{
    #region Private fields

    private readonly IndividualValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateIndividualShouldSucceed()
    {
        var individual = new Individual
        {
            Name = _rndSvc.NextString(0xff),
            AddedBy = new User { AccessLevel = AccessLevel.Group }
        };

        Assert.IsTrue(_validator.Validate(individual).IsValid);
    }

    [TestMethod]
    public void ValidateIndividualShouldFail()
    {
        var individual = new Individual
        {
            Name = "",
            AddedBy = new User { AccessLevel = AccessLevel.Farmer }
        };

        var result = _validator.TestValidate(individual);

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.AddedBy.AccessLevel);
    }

    #endregion Tests
}