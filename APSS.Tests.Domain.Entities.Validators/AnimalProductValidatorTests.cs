using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Domain.Services;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class AnimalProductValidatorTests
{
    #region Private fields

    private readonly AnimalProductValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateAnimalProductShouldSucceed()
    {
        var animalProduct = new AnimalProduct
        {
            Name = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
        };

        Assert.IsTrue(_validator.Validate(animalProduct).IsValid);
    }

    [TestMethod]
    public void ValidateAnimalproductShouldFail()
    {
        var animalProduct = new AnimalProduct
        {
            Name = "",
            Quantity = -2,
        };

        var result = _validator.TestValidate(animalProduct);

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.Quantity);
    }

    #endregion Tests
}