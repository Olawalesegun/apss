using Microsoft.VisualStudio.TestTools.UnitTesting;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Validators;
using APSS.Domain.Services;
using APSS.Tests.Utils;

using FluentValidation.TestHelper;

namespace APSS.Tests.Domain.Entities.Validators;

[TestClass]
public class AccountValidatorTests
{
    #region Private fields

    private readonly AccountValidator _validator = new();
    private readonly SimpleRandomGeneratorService _rndSvc = new();

    #endregion Private fields

    #region Tests

    [TestMethod]
    public void ValidateAccountShouldSucceed()
    {
        var account = new Account
        {
            HolderName = _rndSvc.NextString(0xff),
            NationalId = _rndSvc.NextString(0xff),
        };

        Assert.IsTrue(_validator.Validate(account).IsValid);
    }

    [TestMethod]
    public void ValidateAccountShouldFail()
    {
        var account = new Account
        {
            HolderName = "",
            NationalId = "",
        };

        var result = _validator.TestValidate(account);

        result.ShouldHaveValidationErrorFor(l => l.HolderName);
        result.ShouldHaveValidationErrorFor(l => l.NationalId);
    }

    #endregion Tests
}