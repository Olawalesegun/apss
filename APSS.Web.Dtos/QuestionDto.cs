using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos
{
    public class QuestionDto : BaseAuditbleDto
    {
        [Display(Name = "Squence Number")]
        public uint Index { get; set; }

        [Display(Name = "Question")]
        public string Text { get; set; } = null!;

        [Display(Name = "Is Required")]
        public bool IsRequired { get; set; } = false;

        [Display(Name = "Survey")]
        public Survey Survey { get; set; } = null!;

        [Display(Name = "Question Type")]
        public QuestionTypeDto QuestionType { get; set; }

        [Display(Name = "Choices")]
        public ICollection<string>? CandidateAnswers { get; set; }

        [Display(Name = "Select More one?")]
        public bool CanMultiSelect { get; set; }
    }
}