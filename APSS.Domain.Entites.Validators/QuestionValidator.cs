﻿using FluentValidation;

namespace APSS.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Question"/>
/// </summary>
public sealed class QuestionValidator : Validator<Question>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public QuestionValidator()
    {
       
    }
}