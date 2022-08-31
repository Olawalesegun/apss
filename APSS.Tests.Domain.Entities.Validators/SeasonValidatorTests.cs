using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class SeasonValidatorTests
{
    #region Private fields

    private readonly SeasonValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateSeasonShouldSucceed()
    {
        var season = new Season
        {
            Name = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(season).IsValid);
    }

    [TestMethod]
    public void ValidateSeasonShouldFail()
    {
        var season = new Season
        {
            Name = "",
        };

        var result = _validator.TestValidate(season);

        result.ShouldHaveValidationErrorFor(l => l.Name);
    }

    #endregion Tests
}