﻿using APSS.Domain.Entities;
using APSS.Domain.Repositories;

namespace APSS.Domain.Services;

/// <summary>
/// An interface to represent survey managment service
/// </summary>
public interface ISurveysService
{
    #region Public Methods

    /// <summary>
    /// Asynchrnously adds a logical (true/false) question to a survey
    /// </summary>
    /// <param name="accountId">The id of the account adding the question</param>
    /// <param name="surveyId">The id of the survey to add the question to</param>
    /// <param name="text">The text of the question</param>
    /// <param name="isRequired">Whether question is required or not</param>
    /// <returns>The created question object</returns>
    Task<LogicalQuestion> AddLogicalQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired);

    /// <summary>
    /// Asynchrnously adds a multiple-choice question to a survey
    /// </summary>
    /// <param name="accountId">The id of the account adding the question</param>
    /// <param name="surveyId">The id of the survey to add the question to</param>
    /// <param name="text">The text of the question</param>
    /// <param name="isRequired">Whether question is required or not</param>
    /// <param name="canMultiSelect">Whether the question allows multi-select or not</param>
    /// <param name="candidateAnswers">The question candidate answers</param>
    /// <returns>The created question object</returns>
    Task<MultipleChoiceQuestion> AddMultipleChoiceQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired,
        bool canMultiSelect,
        List<string> candidateAnswers);

    /// <summary>
    /// Asynchrnously adds a text question to a survey
    /// </summary>
    /// <param name="accountId">The id of the account adding the question</param>
    /// <param name="surveyId">The id of the survey to add the question to</param>
    /// <param name="text">The text of the question</param>
    /// <param name="isRequired">Whether question is required or not</param>
    /// <returns>The created question object</returns>
    Task<TextQuestion> AddTextQuestionAsync(
        long accountId,
        long surveyId,
        string text,
        bool isRequired);

    /// <summary>
    /// Asynchrnously creates an answer for a logical question
    /// </summary>
    /// <param name="accountId">The id of the account answering the question</param>
    /// <param name="entryId">The id of the entry to add the answer to</param>
    /// <param name="questionId">The id of the question</param>
    /// <param name="answer">The answer of the question</param>
    /// <returns>The created answer object</returns>
    Task<LogicalQuestionAnswer> AnswerLogicalQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        bool? answer);

    /// <summary>
    /// Asynchrnously creates an answer for a multiple choice question
    /// </summary>
    /// <param name="accountId">The id of the account answering the question</param>
    /// <param name="entryId">The id of the entry to add the answer to</param>
    /// <param name="questionId">The id of the question</param>
    /// <param name="answerItemsIds">The ids of the selected answers</param>
    /// <returns>The created answer object</returns>
    Task<MultipleChoiceQuestionAnswer> AnswerMultipleChoiceQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        params long[] answerItemsIds);

    /// <summary>
    /// Asynchrnously creates an answer for a text question
    /// </summary>
    /// <param name="accountId">The id of the account answering the question</param>
    /// <param name="entryId">The id of the entry to add the answer to</param>
    /// <param name="questionId">The id of the question</param>
    /// <param name="answer">The answer of the question</param>
    /// <returns>The created answer object</returns>
    Task<TextQuestionAnswer> AnswerTextQuestionAsync(
        long accountId,
        long entryId,
        long questionId,
        string? answer);

    /// <summary>
    ///
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="surveyId"></param>
    /// <returns></returns>
    Task<IQueryBuilder<Question>> GetQuestionsSurveysAsync(long accountId, long surveyId);

    /// <summary>
    /// Asynchronously creates a new survey
    /// </summary>
    /// <param name="accountId">The id of the user creating the survey</param>
    /// <param name="name">The name of the survey</param>
    /// <param name="expiresAt">The expiration date of the survey</param>
    /// <returns>The created survey object</returns>
    Task<Survey> CreateSurveyAsync(long accountId, string name, DateTime expiresAt);

    /// <summary>
    /// Asynchronsously creates a survey entry for a user
    /// </summary>
    /// <param name="accountId">The id of the account creating the entry</param>
    /// <param name="surveyId">The id of the survey to create the entry for</param>
    /// <returns>The created survey object</returns>
    Task<SurveyEntry> CreateSurveyEntryAsync(long accountId, long surveyId);

    /// <summary>
    /// Asynchrnously gets availble surveys for a specific user
    /// </summary>
    /// <param name="accountId">The id of the account to get surveys for</param>
    /// <returns>A query builder with the matching surveys for the user</returns>
    Task<IQueryBuilder<Survey>> GetAvailableSurveysAsync(long accountId);

    /// <summary>
    /// Asynchrnously gets  surveys for a specific user
    /// </summary>
    /// <param name="accountId">The id of the account to get surveys for</param>
    /// <returns>A query builder with the matching surveys for the user</returns>
    Task<IQueryBuilder<Survey>> GetSurveysAsync(long accountId);

    /// <summary>
    /// Asynchronously gets a survey by id
    /// </summary>
    /// <param name="accountId">the id of the account who wants to access the survey</param>
    /// <param name="surveyId">The id of the survey to acess</param>
    /// <returns>A query builder to the relevant survey</returns>
    Task<Survey> GetSurveyAsync(long accountId, long surveyId);

    /// <summary>
    /// Asynchrnonously gets survey entries for a user
    /// </summary>
    /// <param name="accountId">The id of the account which to get the entries for</param>
    /// <returns>A query builder for the survey entries of the user</returns>
    Task<IQueryBuilder<SurveyEntry>> GetSurveyEntriesAsync(long accountId);

    /// <summary>
    /// Asynchronously checks whether an entry is complete or not
    /// </summary>
    /// <param name="accountId">The id of the account owning the entry</param>
    /// <param name="surveyEntryId">The id of the entry to check its completence</param>
    /// <returns>True if the entry is complete, false otherwise</returns>
    Task<bool> IsEntryComplete(long accountId, long surveyEntryId);

    /// <summary>
    /// Asynchrnously removes a survey
    /// </summary>
    /// <param name="accountId">The id of the account removing the survey</param>
    /// <param name="surveyId">The id of the servey to remove</param>
    /// <returns></returns>
    Task RemoveSurveyAsync(long accountId, long surveyId);

    /// <summary>
    /// Asynchrnously sets the active status of a survey
    /// </summary>
    /// <param name="accountId">The id of the account setting the survey active status</param>
    /// <param name="surveyId">The id of the survey to change its active status</param>
    /// <param name="activeStatus">The new active status</param>
    /// <returns>The updated survey object</returns>
    Task<Survey> SetSurveyActiveStatusAsync(long accountId, long surveyId, bool activeStatus);

    /// <summary>
    /// Asynchrnously update the survey
    /// </summary>
    /// <param name="accountId">The id of the account update the survey</param>
    /// <param name="surveyId"></param>
    /// <param name="updater"></param>
    /// <returns></returns>
    Task<Survey> UpdateSurveyAsync(long accountId, long surveyId, Action<Survey> updater);

    /// <summary>
    /// Asynchrnously removes a survey
    /// </summary>
    /// <param name="accountId">The id of the account removing the survey</param>
    /// <param name="questionId">The id of the question to remove</param>
    /// <returns></returns>
    Task RemoveQuestion(long accountId, long questionId);

    Task<Question> GetQuestionAsync(long accountId, long questionId);

    Task<Question> UpdateQuestionAsync(long accountId, long questionId, Action<Question> updater);

    Task<MultipleChoiceAnswerItem> UpdateItemsAnswerAsync(long itemId, Action<MultipleChoiceAnswerItem> updater);

    Task<ICollection<MultipleChoiceAnswerItem>> GetItemsAnswer(long accountId, long questionId);

    Task<SurveyEntry> GetSurveyEntryAsync(long accountId, long entryId);

    Task<MultipleChoiceQuestion> UpdateMultipleChoiceQuestion(long questionId, Action<MultipleChoiceQuestion> updater);

    #endregion Public Methods
}