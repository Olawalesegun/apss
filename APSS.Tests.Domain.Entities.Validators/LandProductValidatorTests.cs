using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class LandProductValidatorTests
{
    #region Private fields

    private readonly LandProductValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateLandProductShouldSucceed()
    {
        var landProduct = new LandProduct
        {
            CropName = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
            HarvestStart = System.DateTime.Now,
            HarvestEnd = System.DateTime.Now.AddHours(2),
        };

        Assert.IsTrue(_validator.Validate(landProduct).IsValid);
    }

    [TestMethod]
    public void ValidateLandProductShouldFail()
    {
        var landProduct = new LandProduct
        {
            CropName = "",
            Quantity = 0
        };

        var result = _validator.TestValidate(landProduct);

        result.ShouldHaveValidationErrorFor(a => a.CropName);
        result.ShouldHaveValidationErrorFor(a => a.Quantity);
    }

    #endregion Tests
}