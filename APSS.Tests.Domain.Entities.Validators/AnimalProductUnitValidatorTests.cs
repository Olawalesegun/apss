using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Domain.Services;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class AnimalProductUnitValidatorTests
{
    #region Private fields

    private readonly AnimalProductUnitValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateAnimalProductUnitShouldSucceed()
    {
        var animalProductUnit = new AnimalProductUnit
        {
            Name = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(animalProductUnit).IsValid);
    }

    [TestMethod]
    public void ValidateAnimalProductUnitShouldFail()
    {
        var animalProductUnit = new AnimalProductUnit
        {
            Name = "",
        };

        var result = _validator.TestValidate(animalProductUnit);

        result.ShouldHaveValidationErrorFor(l => l.Name);
    }

    #endregion Tests
}