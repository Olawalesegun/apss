﻿using APSS.Domain.Entities;

namespace APSS.Domain.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Gets the users repository
    /// </summary>
    IRepository<User> Users { get; }

    /// <summary>
    /// Gets the users' permission inheritances repository
    /// </summary>
    IRepository<PermissionInheritance> PermissionInheritances { get; }

    /// <summary>
    /// Gets the lands repository
    /// </summary>
    IRepository<Land> Lands { get; }

    /// <summary>
    /// Gets the animal groups repository
    /// </summary>
    IRepository<AnimalGroup> AnimalGroups { get; }

    /// <summary>
    /// Gets the products repository
    /// </summary>
    IRepository<Product> Products { get; }

    /// <summary>
    /// Gets the produt expenses repository
    /// </summary>
    IRepository<ProductExpense> ProductExpenses { get; }

    /// <summary>
    /// Gets the land products repository
    /// </summary>
    IRepository<LandProduct> LandProducts { get; }

    /// <summary>
    /// Gets the seasons repository
    /// </summary>
    IRepository<Season> Sessions { get; }

    /// <summary>
    /// Gets the animal groups repositories
    /// </summary>
    IRepository<AnimalProduct> AnimalProducts { get; }

    /// <summary>
    /// Gets the surveys repository
    /// </summary>
    IRepository<Survey> Surveys { get; }

    /// <summary>
    /// Gets the questions repository
    /// </summary>
    IRepository<Question> Questions { get; }

    /// <summary>
    /// Gets the multiple-choice questions repository
    /// </summary>
    IRepository<MultipleChoiceQuestion> MultipleChoiceQuestions { get; }

    /// <summary>
    /// Gets the multiple-choice questions' answer items repository
    /// </summary>
    IRepository<MultipleChoiceAnswerItem> MultipleChoiceAnswerItems { get; }

    /// <summary>
    /// Gets the logical questions repository
    /// </summary>
    IRepository<LogicalQuestion> LogicalQuestions { get; }

    /// <summary>
    /// Gets the text questions repository
    /// </summary>
    IRepository<TextQuestion> TextQuestions { get; }

    /// <summary>
    /// Gets the survey entries repository
    /// </summary>
    IRepository<SurveyEntry> SurveyEntries { get; }

    /// <summary>
    /// Gets the individuals repository
    /// </summary>
    IRepository<Individual> Individuals { get; }

    /// <summary>
    /// Gets the skills repository
    /// </summary>
    IRepository<Skill> Skills { get; }

    /// <summary>
    /// Gets the volantaries repository
    /// </summary>
    IRepository<Voluntary> Volantaries { get; }

    /// <summary>
    /// Gets the families repository
    /// </summary>
    IRepository<Family> Families { get; }

    /// <summary>
    /// Gets the family individuals repository
    /// </summary>
    IRepository<FamilyIndividual> FamiliesIndividuals { get; }

    /// <summary>
    /// Gets the logs repository
    /// </summary>
    IRepository<Log> Logs { get; }

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync();

    /// <summary>
    /// Asynchronously commits changes to data backend
    /// </summary>
    /// <returns>The affected records count</returns>
    Task<int> CommitAsync(IDatabaseTransaction transaction, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously begins a transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}