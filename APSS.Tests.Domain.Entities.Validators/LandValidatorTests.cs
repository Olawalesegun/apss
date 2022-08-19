using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class LandValidatorTests
{
    #region Private fields

    private readonly LandValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateLandShouldSucceed()
    {
        var land = new Land
        {
            Name = _rndSvc.NextString(0xff),
            OwnedBy = new User { AccessLevel = AccessLevel.Farmer },
            Latitude = _rndSvc.NextInt32(-90, 90),
            Longitude = _rndSvc.NextInt32(-180, 180)
        };

        Assert.IsTrue(_validator.Validate(land).IsValid);
    }

    [TestMethod]
    public void ValidateLandShouldFail()
    {
        var land = new Land
        {
            Name = "",
            OwnedBy = new User { AccessLevel = AccessLevel.Group },
            Latitude = _rndSvc.NextInt32(-100),
            Longitude = _rndSvc.NextInt32(200)
        };

        var result = _validator.TestValidate(land);

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.OwnedBy.AccessLevel);
        result.ShouldHaveValidationErrorFor(a => a.Longitude);
        result.ShouldHaveValidationErrorFor(a => a.Latitude);
    }

    #endregion Tests
}