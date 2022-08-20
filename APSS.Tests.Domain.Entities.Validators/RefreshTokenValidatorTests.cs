using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class RefreshTokenValidatorTests
{
    #region Private fields

    private readonly RefreshTokenValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateAccountShouldSucceed()
    {
        var refreshToken = new RefreshToken
        {
            Token = _rndSvc.NextString(0xff),
            LastLogin = DateTime.Now.Subtract(TimeSpan.FromSeconds(_rndSvc.NextInt32(0))),
            ValidUntil = DateTime.Now.Add(TimeSpan.FromSeconds(_rndSvc.NextInt32(1))),
        };

        Assert.IsTrue(_validator.Validate(refreshToken).IsValid);
    }

    [TestMethod]
    public void ValidateAccountShouldFail()
    {
        var refreshToken = new RefreshToken
        {
            Token = string.Empty,
            LastLogin = DateTime.Now.Add(TimeSpan.FromSeconds(_rndSvc.NextInt32(0))),
            ValidUntil = DateTime.Now.Subtract(TimeSpan.FromSeconds(_rndSvc.NextInt32(1))),
        };

        var result = _validator.TestValidate(refreshToken);

        result.ShouldHaveValidationErrorFor(r => r.Token);
        result.ShouldHaveValidationErrorFor(r => r.LastLogin);
        result.ShouldHaveValidationErrorFor(r => r.ValidUntil);
    }

    #endregion Tests
}