﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Domain.Services;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class AnimalGroupValidatorTests
{
    #region Private fields

    private readonly AnimalGroupValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateAnimalGroupShouldSucceed()
    {
        var animalGroup = new AnimalGroup
        {
            Name = _rndSvc.NextString(0xff),
            Type = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
            OwnedBy = new User { AccessLevel = AccessLevel.Farmer }
        };

        Assert.IsTrue(_validator.Validate(animalGroup).IsValid);
    }

    [TestMethod]
    public void ValidateAnimalGroupShouldFail()
    {
        var animalGroup = new AnimalGroup
        {
            Name = "",
            Type = " ",
            Quantity = -2,
            OwnedBy = new User { AccessLevel = AccessLevel.Group }
        };

        var result = _validator.TestValidate(animalGroup);

        result.ShouldHaveValidationErrorFor(a => a.Name);
        result.ShouldHaveValidationErrorFor(a => a.Type);
        result.ShouldHaveValidationErrorFor(a => a.Quantity);
        result.ShouldHaveValidationErrorFor(a => a.OwnedBy.AccessLevel);
    }

    #endregion Tests
}