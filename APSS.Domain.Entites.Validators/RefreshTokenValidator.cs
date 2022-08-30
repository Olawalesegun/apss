using FluentValidation;

namespace APSS.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="RefreshToken"/>
/// </summary>
public sealed class RefreshTokenValidator : Validator<RefreshToken>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public RefreshTokenValidator()
    {
        RuleFor(r => r.Value)
            .NotEmpty()
            .WithMessage("the token cannot be empty");

        RuleFor(r => r.LastLogin)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("last login cannot be in the future");

        RuleFor(r => r.ValidUntil)
            .GreaterThan(DateTime.Now)
            .WithMessage("validity cannot be in the past");
    }
}