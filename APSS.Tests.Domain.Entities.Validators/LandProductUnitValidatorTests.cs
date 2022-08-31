﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class LandProductUnitValidatorTests
{
    #region Private fields

    private readonly LandProductUnitValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateLandProductUnitShouldSucceed()
    {
        var landProductUnit = new LandProductUnit
        {
            Name = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(landProductUnit).IsValid);
    }

    [TestMethod]
    public void ValidateLandProductUnitProShouldFail()
    {
        var landProductUnit = new LandProductUnit
        {
            Name = "",
        };

        var result = _validator.TestValidate(landProductUnit);

        result.ShouldHaveValidationErrorFor(l => l.Name);
    }

    #endregion Tests
}