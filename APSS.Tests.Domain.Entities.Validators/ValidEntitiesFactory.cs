using System;
using System.Collections.Generic;
using System.Linq;

using APSS.Domain.Entities;
using APSS.Tests.Utils;

namespace APSS.Tests.Domain.Entities.Validators;

public static class ValidEntitiesFactory
{
    private static readonly SimpleRandomGeneratorService _rndSvc = new();

    /// <summary>
    /// Creates a valid user object
    /// </summary>
    /// <param name="accessLevel">The user access level</param>
    /// <returns>The created user object</returns>
    public static User CreateValidUser(AccessLevel accessLevel)
    {
        return new User
        {
            Name = _rndSvc.NextString(30),
            AccessLevel = accessLevel,
            SupervisedBy = accessLevel != AccessLevel.Root
                ? CreateValidUser(accessLevel.NextLevelUpove())
                : null,
        };
    }

    /// <summary>
    /// Creates a valid account object
    /// </summary>
    /// <param name="permissions">The account permissions</param>
    /// <returns>The created account object</returns>
    public static Account CreateValidAccount(PermissionType permissions)
    {
        return new Account
        {
            HolderName = _rndSvc.NextString(0xff),
            NationalId = _rndSvc.NextString(0xff),
            PasswordHash = _rndSvc.NextString(0x7f),
            PasswordSalt = _rndSvc.NextString(0x7f),
            PhoneNumber = string.Empty,
            SocialStatus = SocialStatus.Unspecified,
            Job = string.Empty,
            Permissions = permissions,
            User = CreateValidUser(_rndSvc.NextAccessLevel()),
        };
    }

    /// <summary>
    /// Creates a valid animal group object
    /// </summary>
    /// <returns>The created animal group object</returns>
    public static AnimalGroup CreateValidAnimalGroup()
    {
        return new AnimalGroup
        {
            Name = _rndSvc.NextString(0xff),
            Type = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
            OwnedBy = CreateValidUser(AccessLevel.Farmer),
        };
    }

    /// <summary>
    /// Creates a valid animal product object
    /// </summary>
    /// <returns>The created animal product object</returns>
    public static AnimalProduct CreateValidAnimalProduct()
    {
        return new AnimalProduct
        {
            Name = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
            Unit = CreateValidAnimalProductUnit(),
            PeriodTaken = new TimeSpan(_rndSvc.NextInt64()),
            Producer = CreateValidAnimalGroup(),
        };
    }

    /// <summary>
    /// Creates a valid animal product unit object
    /// </summary>
    /// <returns>The created animal product unit object</returns>
    public static AnimalProductUnit CreateValidAnimalProductUnit()
    {
        return new AnimalProductUnit
        {
            Name = _rndSvc.NextString(0xff),
        };
    }

    /// <summary>
    /// Creates a valid family object
    /// </summary>
    /// <returns>The created family object</returns>
    public static Family CreateValidFamily(User? addedBy = null)
    {
        return new Family
        {
            Name = _rndSvc.NextString(0xff),
            LivingLocation = _rndSvc.NextString(0xff),
            AddedBy = addedBy ?? CreateValidUser(AccessLevel.Group),
        };
    }

    /// <summary>
    /// Creates a valid individual object
    /// </summary>
    /// <returns>The created individual object</returns>
    public static Individual CreateValidIndividual(User? addedBy = null)
    {
        return new Individual
        {
            Name = _rndSvc.NextString(0xff),
            Sex = IndividualSex.Male,
            DateOfBirth = DateTime.Now,
            Address = _rndSvc.NextString(0xff),
            SocialStatus = SocialStatus.Unspecified,
            AddedBy = addedBy ?? CreateValidUser(AccessLevel.Group),
        };
    }

    /// <summary>
    /// Creates a valid land object
    /// </summary>
    /// <returns>The created land object</returns>
    public static Land CreateValidLand()
    {
        return new Land
        {
            Name = _rndSvc.NextString(0xff),
            Address = _rndSvc.NextString(0xff),
            Area = _rndSvc.NextInt64(),
            Latitude = _rndSvc.NextFloat64(-90, 90),
            Longitude = _rndSvc.NextFloat64(-180, 180),
            IsUsable = true,
            IsUsed = true,
            OwnedBy = CreateValidUser(AccessLevel.Farmer),
        };
    }

    /// <summary>
    /// Creates a valid land product object
    /// </summary>
    /// <returns>The created land product object</returns>
    public static LandProduct CreateValidLandProduct()
    {
        return new LandProduct
        {
            CropName = _rndSvc.NextString(0xff),
            Quantity = _rndSvc.NextInt32(1),
            StoringMethod = _rndSvc.NextString(0xff),
            Unit = CreateValidLandProductUnit(),
            Category = _rndSvc.NextString(0xff),
            IrrigationMethod = _rndSvc.NextString(0xff),
            IrrigationCount = _rndSvc.NextFloat64(),
            IrrigationWaterSource = IrrigationWaterSource.HumanStored,
            IrrigationPowerSource = IrrigationPowerSource.FossileFuel,
            Fertilizer = _rndSvc.NextString(0xff),
            Insecticide = _rndSvc.NextString(0xff),
            Producer = CreateValidLand(),
            ProducedIn = CreateValidSeason(),
            HarvestStart = DateTime.Now,
            HarvestEnd = DateTime.Now.AddHours(2)
        };
    }

    /// <summary>
    /// Creates a valid land product unit object
    /// </summary>
    /// <returns>The created land product unit object</returns>
    public static LandProductUnit CreateValidLandProductUnit()
    {
        return new LandProductUnit
        {
            Name = _rndSvc.NextString(0xff),
        };
    }

    /// <summary>
    /// Creates a valid family individual object
    /// </summary>
    /// <returns>The created family individual object</returns>
    public static FamilyIndividual CreateValidFamilyIndividual(User? addedBy = null)
    {
        addedBy ??= CreateValidUser(AccessLevel.Group);

        return new FamilyIndividual
        {
            Individual = CreateValidIndividual(addedBy),
            Family = CreateValidFamily(addedBy),
            IsParent = _rndSvc.NextBool(),
            IsProvider = _rndSvc.NextBool(),
        };
    }

    /// <summary>
    /// Creates a valid season object
    /// </summary>
    /// <returns>The created season object</returns>
    public static Season CreateValidSeason()
    {
        return new Season
        {
            Name = _rndSvc.NextString(0xff),
            StartsAt = DateTime.Now,
            EndsAt = DateTime.Now
        };
    }

    /// <summary>
    /// Creates a valid log object
    /// </summary>
    /// <returns>The created log object</returns>
    public static Log CreateValidLog()
    {
        return new Log
        {
            Message = _rndSvc.NextString(0xff),
            TimeStamp = DateTime.Now,
        };
    }

    /// <summary>
    /// Creates a valid product expense object
    /// </summary>
    /// <returns>The created product expense object</returns>
    public static ProductExpense CreateValidProductExpense()
    {
        return new ProductExpense
        {
            Type = _rndSvc.NextString(0xff),
            Price = Convert.ToDecimal(_rndSvc.NextFloat64(0, 1_000_000)),
            SpentOn = _rndSvc.NextBool()
                ? CreateValidLandProduct()
                : CreateValidAnimalProduct()
        };
    }

    /// <summary>
    /// Creates a valid skill object
    /// </summary>
    /// <returns>The created skill object</returns>
    public static Skill CreateValidSkill()
    {
        return new Skill
        {
            Name = _rndSvc.NextString(0xff),
            Field = _rndSvc.NextString(0xff),
            BelongsTo = CreateValidIndividual()
        };
    }

    /// <summary>
    /// Creates a valid voluntary object
    /// </summary>
    /// <returns>The created voluntary object</returns>
    public static Voluntary CreateValidVoluntary()
    {
        return new Voluntary
        {
            Name = _rndSvc.NextString(0xff),
            Field = _rndSvc.NextString(0xff),
            OfferedBy = CreateValidIndividual()
        };
    }

    /// <summary>
    /// Creates a valid survey object
    /// </summary>
    /// <returns>The created survey object</returns>
    public static Survey CreateValidSurvey(TimeSpan validFor)
    {
        return new Survey
        {
            Name = _rndSvc.NextString(0xff),
            ExpirationDate = DateTime.Now.Add(validFor),
            CreatedBy = CreateValidUser(AccessLevel.Group),
        };
    }

    /// <summary>
    /// Creates a valid logical question object
    /// </summary>
    /// <returns>The created logical question object</returns>
    public static LogicalQuestion CreateValidLogicalQuestion(bool IsRequired)
    {
        return new LogicalQuestion
        {
            Index = (uint)_rndSvc.NextInt32(0),
            Text = _rndSvc.NextString(0xff),
            IsRequired = IsRequired
        };
    }

    /// <summary>
    /// Creates a valid text question object
    /// </summary>
    /// <returns>The created text question object</returns>
    public static TextQuestion CreateValidTextQuestion(bool IsRequired)
    {
        return new TextQuestion
        {
            Index = (uint)_rndSvc.NextInt32(0),
            Text = _rndSvc.NextString(0xff),
            IsRequired = IsRequired
        };
    }

    /// <summary>
    /// Creates a valid multiple choice answer item object
    /// </summary>
    /// <returns>The created multiple choice answer item object</returns>
    public static MultipleChoiceAnswerItem CreateValidMultipleChoiceAnswerItem()
    {
        return new MultipleChoiceAnswerItem
        {
            Value = _rndSvc.NextString(0xff),
        };
    }

    /// <summary>
    /// Creates a valid multiple choice question object
    /// </summary>
    /// <returns>The created multiple choice question object</returns>
    public static MultipleChoiceQuestion CreateValidMultipleChoiceQuestion(bool IsRequired)
    {
        return new MultipleChoiceQuestion
        {
            Index = (uint)_rndSvc.NextInt32(0),
            Text = _rndSvc.NextString(0xff),
            IsRequired = IsRequired,
            CandidateAnswers = Enumerable.Range(2, _rndSvc.NextInt32(2, 6))
                .Select(i => CreateValidMultipleChoiceAnswerItem())
                .ToList(),
            CanMultiSelect = _rndSvc.NextBool(),
        };
    }

    /// <summary>
    /// Creates a valid logical question answer object
    /// </summary>
    /// <returns>The created logical question answer object</returns>
    public static LogicalQuestionAnswer CreateValidLogicalQuestionAnswer()
    {
        return new LogicalQuestionAnswer
        {
            Question = CreateValidLogicalQuestion(true),
            Answer = _rndSvc.NextBool(),
        };
    }

    /// <summary>
    /// Creates a valid text question answer object
    /// </summary>
    /// <returns>The created text question answer object</returns>
    public static TextQuestionAnswer CreateValidTextQuestionAnswer()
    {
        return new TextQuestionAnswer
        {
            Question = CreateValidTextQuestion(true),
            Answer = _rndSvc.NextString(0xff),
        };
    }

    /// <summary>
    /// Creates a valid multiple choice question answer object
    /// </summary>
    /// <returns>The created multiple choice question answer object</returns>
    public static MultipleChoiceQuestionAnswer CreateValidMultipleChoiceQuestionAnswer()
    {
        var question = CreateValidMultipleChoiceQuestion(true);

        return new MultipleChoiceQuestionAnswer
        {
            Question = question,
            Answers = question.CanMultiSelect
                ? question
                    .CandidateAnswers
                    .Take(_rndSvc.NextInt32(2, question.CandidateAnswers.Count))
                    .ToList()
                : new List<MultipleChoiceAnswerItem> { question.CandidateAnswers.First() },
        };
    }
}