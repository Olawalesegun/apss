using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

/// <summary>
/// An service to manage surveys on the application
/// </summary>
public sealed class SurveysService : ISurveysService
{
    #region Fields

    private readonly IPermissionsService _permissionsSvc;
    private readonly IUnitOfWork _uow;
    private readonly IUsersService _usersSvc;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    /// <param name="permissionsSvc">The application permissions management service</param>
    /// <param name="usersSvc">Users managment service</param>
    public SurveysService(IUnitOfWork uow, IPermissionsService permissionsSvc, IUsersService usersSvc)
    {
        _uow = uow;
        _permissionsSvc = permissionsSvc;
        _usersSvc = usersSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public Task<LogicalQuestion> AddLogicalQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired)
    {
        return AddQuestionAsync(_uow.LogicalQuestions, accountId, surveyId, text, isRequired);
    }

    public async Task<ICollection<MultipleChoiceAnswerItem>> GetItemsAnswer(long accountId, long questionId)
    {
        var answers = await _uow.MultipleChoiceQuestions.Query()
            .Include(a => a.CandidateAnswers)
            .FindAsync(questionId);

        return answers.CandidateAnswers;
    }

    /// <inheritdoc/>
    public async Task<MultipleChoiceQuestion> AddMultipleChoiceQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired,
        bool canMultiSelect,
        List<string> candidateAnswers)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var answers = candidateAnswers.Select(a => new MultipleChoiceAnswerItem
        {
            Value = a,
        }).ToArray();

        _uow.MultipleChoiceAnswerItems.Add(answers);

        var question = await AddQuestionAsync(
            _uow.MultipleChoiceQuestions,
            accountId,
            surveyId,
            text,
            isRequired,
            q =>
            {
                q.CandidateAnswers = answers;
                q.CanMultiSelect = canMultiSelect;
            });

        await _uow.CommitAsync(tx);

        return question;
    }

    /// <inheritdoc/>
    public Task<TextQuestion> AddTextQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired)
    {
        return AddQuestionAsync(_uow.TextQuestions, accountId, surveyId, text, isRequired);
    }

    /// <inheritdoc/>
    public async Task<LogicalQuestionAnswer> AnswerLogicalQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        bool? answer)
    {
        var (account, entry) = await GetAnswerEntryAsync(accountId, entryId);

        var question = await _uow.LogicalQuestions.Query()
            .Where(q => q.Survey.Id == entry.Survey.Id)
            .FindAsync(questionId);

        if (question.IsRequired && answer is null)
        {
            throw new InvalidLogicalQuestionAnswerException(
                questionId,
                answer,
                $"user #{account.User.Id} with account #{accountId} has tried to answer a required logical question #{questionId} of survey #{entry.Survey.Id} on entry #{entry.Id} with a null value");
        }

        var answerObj = await _uow.LogicalQuestionAnswers.Query()
            .Where(a => a.Question.Id == questionId)
            .FirstOrNullAsync();

        if (answerObj is not null)
        {
            answerObj.Answer = answer;
            _uow.LogicalQuestionAnswers.Update(answerObj);
        }
        else
        {
            answerObj = new LogicalQuestionAnswer
            {
                Question = question,
                Answer = answer,
            };

            entry.Answers.Add(answerObj);

            _uow.LogicalQuestionAnswers.Add(answerObj);
            _uow.SurveyEntries.Update(entry);
        }

        await _uow.CommitAsync();

        return answerObj;
    }

    /// <inheritdoc/>
    public async Task<MultipleChoiceQuestionAnswer> AnswerMultipleChoiceQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        params long[] answerItemsIds)
    {
        var (account, entry) = await GetAnswerEntryAsync(accountId, entryId);

        var question = await _uow.MultipleChoiceQuestions.Query()
            .Where(q => q.Survey.Id == entry.Survey.Id)
            .FindAsync(questionId);

        var answerObj = await _uow.MultipleChoiceQuestionAnswers.Query()
            .Where(a => a.Question.Id == questionId)
            .FirstOrNullAsync();

        var answerItems = await Task.WhenAll(answerItemsIds
            .Select(i => _uow.MultipleChoiceAnswerItems.Query().FindAsync(i)));

        if (question.IsRequired && answerItems.Length == 0)
        {
            throw new InvalidMultipleChoiceQuestionAnswerException(
                questionId,
                answerItems.Select(i => i.Value),
                $"user #{account.User.Id} with account #{accountId} has tried to answer a required multiple choice question #{questionId} of survey #{entry.Survey.Id} on entry #{entry.Id} with an empty value");
        }
        else if (!question.CanMultiSelect && answerItems.Length > 1)
        {
            throw new InvalidMultipleChoiceQuestionAnswerException(
                questionId,
                answerItems.Select(i => i.Value),
                $"user #{account.User.Id} with account #{accountId} has tried to answer a multiple-choice question #{questionId} (no multi-select) of survey #{entry.Survey.Id} on entry #{entry.Id} with multiple values");
        }

        if (answerObj is not null)
        {
            answerObj.Answers = answerItems;
            _uow.MultipleChoiceQuestionAnswers.Update(answerObj);
        }
        else
        {
            answerObj = new MultipleChoiceQuestionAnswer
            {
                Question = question,
                Answers = answerItems,
            };

            entry.Answers.Add(answerObj);

            _uow.MultipleChoiceQuestionAnswers.Add(answerObj);
            _uow.SurveyEntries.Update(entry);
        }

        await _uow.CommitAsync();

        return answerObj;
    }

    /// <inheritdoc/>
    public async Task<TextQuestionAnswer> AnswerTextQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        string? answer)
    {
        var (account, entry) = await GetAnswerEntryAsync(accountId, entryId);

        var question = await _uow.TextQuestions.Query()
            .Where(q => q.Survey.Id == entry.Survey.Id)
            .FindAsync(questionId);

        if (question.IsRequired && answer is null)
        {
            throw new InvalidTextQuestionAnswerException(
                questionId,
                answer,
                $"user #{account.User.Id} with account #{accountId} has tried to answer a required text question #{questionId} of survey #{entry.Survey.Id} on entry #{entry.Id} with a null value");
        }

        var answerObj = await _uow.TextQuestionAnswers.Query()
            .Where(a => a.Question.Id == questionId)
            .FirstOrNullAsync();

        if (answerObj is not null)
        {
            answerObj.Answer = answer;
            _uow.TextQuestionAnswers.Update(answerObj);
        }
        else
        {
            answerObj = new TextQuestionAnswer
            {
                Question = question,
                Answer = answer,
            };

            entry.Answers.Add(answerObj);

            _uow.TextQuestionAnswers.Add(answerObj);
            _uow.SurveyEntries.Update(entry);
        }

        await _uow.CommitAsync();

        return answerObj;
    }

    /// <inheritdoc/>
    public async Task<Survey> CreateSurveyAsync(long accountId, string name, DateTime expiresAt)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Create);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(accountId, $"farmer #{account.User.Id} with account #{accountId} cannot add surveys");

        var survey = new Survey
        {
            CreatedBy = account.User,
            Name = name,
            ExpirationDate = expiresAt,
        };

        _uow.Surveys.Add(survey);
        await _uow.CommitAsync();

        return survey;
    }

    /// <inheritdoc/>
    public async Task<SurveyEntry> CreateSurveyEntryAsync(long accountId, long surveyId)
    {
        var account = await _uow.Accounts.Query()
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read | PermissionType.Create);

        var survey = await (await DoGetAvailableSurveysAsync(account.Id)).FindAsync(surveyId);

        var entry = new SurveyEntry
        {
            MadeBy = account.User,
            Survey = survey,
        };

        _uow.SurveyEntries.Add(entry);
        await _uow.CommitAsync();

        return entry;
    }

    /// <inheritdoc/>
    public async Task<IQueryBuilder<Survey>> GetAvailableSurveysAsync(long accountId)
    {
        var account = await _uow.Accounts.Query()
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return await DoGetAvailableSurveysAsync(account.Id);
    }

    public async Task<IQueryBuilder<Survey>> GetSurveysAsync(long accountId)
    {
        var account = await _uow.Accounts.Query()
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        var usersHierarchyIds = await _usersSvc
           .GetUpwardHierarchyAsync(accountId)
           .Select(u => u.Id)
           .ToListAsync();

        return _uow.Surveys.Query()
            .Where(s => usersHierarchyIds.Contains(s.CreatedBy.Id));
    }

    /// <inheritdoc/>
    public async Task<Survey> GetSurveyAsync(long accountId, long surveyId)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return await _uow.Surveys
            .Query()
            .Where(s => s.Id == surveyId && s.CreatedBy.Id == account.User.Id).FirstAsync();
    }

    public async Task<Question> GetQuestionAsync(long accountId, long questionId)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        var question = await _uow.Questions.Query()
            .Include(q => q.Survey)
            .FindAsync(questionId);
        return question;
    }

    /// <inheritdoc/>
    public async Task<IQueryBuilder<SurveyEntry>> GetSurveyEntriesAsync(long accountId)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return _uow.SurveyEntries.Query()
            .Include(e => e.Survey)
            .Include(e => e.MadeBy)
            .Where(e => e.MadeBy.Id == account.User.Id);
    }

    public async Task<IQueryBuilder<Question>> GetQuestionsSurveysAsync(long accountId, long surveyId)
    {
        var account = await _uow.Accounts.Query()
           .Include(a => a.User)
           .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return _uow.Questions.Query()
            .Include(q => q.Survey)
            .Include(q => q.Survey.CreatedBy)
            .Where(q => (q.Survey.Id == surveyId && q.Survey.CreatedBy.Id == account.User.Id));
    }

    /// <inheritdoc/>
    public Task<bool> IsEntryComplete(long accountId, long surveyEntryId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task RemoveSurveyAsync(long accountId, long surveyId)
    {
        var (survey, _) = await GetSurveyWithAuthorizationAsync(accountId, surveyId, PermissionType.Delete);

        var questions = await _uow.Questions.Query()
            .Where(q => q.Survey.Id == surveyId)
            .AsAsyncEnumerable()
            .ToListAsync();

        var questionIds = await questions.Select(q => q.Id)
            .ToAsyncEnumerable()
            .ToListAsync();

        var answers = await _uow.MultipleChoiceQuestions.Query()
              .Include(q => q.CandidateAnswers)
              .Where(q => questionIds.Contains(q.Id))
              .AsAsyncEnumerable()
              .Select(q => q.CandidateAnswers)
              .ToListAsync();

        foreach (var answer in answers)
        {
            var items = answer.ToList().Select(a => a.Id);

            var itemsanswer = await _uow.MultipleChoiceAnswerItems.Query()
                .Where(i => items.Contains(i.Id))
                .AsAsyncEnumerable()
                .ToListAsync();

            itemsanswer.ForEach(_uow.MultipleChoiceAnswerItems.Remove);
        }

        questions.ForEach(_uow.Questions.Remove);

        _uow.Surveys.Remove(survey);
        await _uow.CommitAsync();
    }

    public async Task RemoveQuestion(long accountId, long questionId)
    {
        var question = await _uow.Questions.Query()
            .Include(q => q.Survey).FindAsync(questionId);

        var (survey, _) = await GetSurveyWithAuthorizationAsync(
                            accountId,
                            question.Survey.Id,
                            PermissionType.Delete);
        if (await _uow.MultipleChoiceQuestions
            .Query()
            .AnyAsync(q => q.Id == questionId))
        {
            var items = await _uow.MultipleChoiceQuestions.Query()
                       .Include(a => a.CandidateAnswers)
                       .FindAsync(questionId);
            var itemsId = items.CandidateAnswers.Select(a => a.Id);

            var itemsanswer = await _uow.MultipleChoiceAnswerItems.Query()
               .Where(i => itemsId.Contains(i.Id))
               .AsAsyncEnumerable()
               .ToListAsync();

            itemsanswer.ForEach(_uow.MultipleChoiceAnswerItems.Remove);
        }
        _uow.Questions.Remove(question);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<Survey> SetSurveyActiveStatusAsync(long accountId, long surveyId, bool activeStatus)
    {
        return await UpdateSurveyAsync(accountId, surveyId, s => s.IsActive = activeStatus);
    }

    /// <inheritdoc/>
    public async Task<Survey> UpdateSurveyAsync(long accountId, long surveyId, Action<Survey> updater)
    {
        var survey = await _uow.Surveys.Query().Include(s => s.CreatedBy).FindAsync(surveyId);

        await _permissionsSvc.ValidatePermissionsAsync(accountId, survey.CreatedBy.Id, PermissionType.Update);

        updater(survey);

        _uow.Surveys.Update(survey);
        await _uow.CommitAsync();

        return survey;
    }

    public async Task<Question> UpdateQuestionAsync(long accountId, long questionId, Action<Question> updater)
    {
        var question = await _uow.Questions.Query().Include(q => q.Survey).FindAsync(questionId);

        var (survey, _) = await GetSurveyWithAuthorizationAsync(
            accountId,
            question.Survey.Id,
            PermissionType.Create | PermissionType.Update);

        updater(question);

        _uow.Questions.Update(question);
        await _uow.CommitAsync();

        return question;
    }

    public async Task<MultipleChoiceAnswerItem> UpdateItemsAnswerAsync(long accountId, long itemId, Action<MultipleChoiceAnswerItem> updater)
    {
        var item = await _uow.MultipleChoiceAnswerItems.Query().FindAsync(itemId);

        updater(item);

        _uow.MultipleChoiceAnswerItems.Update(item);
        await _uow.CommitAsync();

        return item;
    }

    #endregion Public Methods

    #region Private Methods

    /// <inheritdoc/>
    private async Task<TQuesiton> AddQuestionAsync<TQuesiton>(
        IRepository<TQuesiton> repo,
        long accountId,
        long surveyId,
        string text,
        bool isRequired,
        Action<TQuesiton>? builder = null) where TQuesiton : Question, new()
    {
        var (survey, _) = await GetSurveyWithAuthorizationAsync(
            accountId,
            surveyId,
            PermissionType.Create | PermissionType.Update);

        var question = new TQuesiton
        {
            Text = text,
            IsRequired = isRequired,
            Survey = survey,
        };

        builder?.Invoke(question);

        repo.Add(question);
        survey.Questions.Add(question);
        _uow.Surveys.Update(survey);
        await _uow.CommitAsync();

        return question;
    }

    private async Task<IQueryBuilder<Survey>> DoGetAvailableSurveysAsync(long accountId)
    {
        var usersHierarchyIds = await _usersSvc
            .GetUpwardHierarchyAsync(accountId)
            .Select(u => u.Id)
            .ToListAsync();

        return _uow.Surveys.Query()
            .Where(s => s.ExpirationDate > DateTime.Now && usersHierarchyIds.Contains(s.CreatedBy.Id));
    }

    private async Task<(Account, SurveyEntry)> GetAnswerEntryAsync(long accountId, long entryId)
    {
        var entry = await _uow.SurveyEntries.Query()
            .Include(e => e.MadeBy)
            .Include(e => e.Survey)
            .FindAsync(entryId);

        var account = await _permissionsSvc.ValidatePermissionsAsync(
            accountId,
            entry.MadeBy.Id,
            PermissionType.Read | PermissionType.Update | PermissionType.Create);

        return (account, entry);
    }

    private async Task<(Survey, Account)> GetSurveyWithAuthorizationAsync(long accountId, long surveyId, PermissionType permissions)
    {
        var survey = await _uow.Surveys
            .Query()
            .Include(s => s.CreatedBy)
            .FindAsync(surveyId);

        var account = await _permissionsSvc.ValidatePermissionsAsync(accountId, survey.CreatedBy.Id, permissions);

        return (survey, account);
    }

    #endregion Private Methods
}