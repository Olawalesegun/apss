using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AccountDto : BaseAuditbleDto
    {
        /// <summary>
        /// Gets or sets the name of the peron who holds this account
        /// </summary>
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        [Display(Name = "اسم المستخدم")]
        public string HolderName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password hash of the user
        /// </summary>
        [Required(ErrorMessage = "يجب ادخال كلمة السر")]
        [Display(Name = "كلمة السر")]
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// Gets or sets the national id of the user
        /// </summary>
        [Required(ErrorMessage = "يجب ادخال رقم البطاقة")]
        [Display(Name = "رقم البطاقة")]
        public string? NationalId { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user
        /// </summary>
        [Required(ErrorMessage = "يجب ادخال رقم التلفون")]
        [Display(Name = "رقم التلفوت")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the social status of the user
        /// </summary>
        [Required(ErrorMessage = "يجب اختيار الحالة الاجتماعية ")]
        [Display(Name = "الحالة الاجتماعية")]
        public SocialStatus SocialStatus { get; set; } = SocialStatus.Unspecified;

        /// <summary>
        /// Gets or sets the job of the user
        /// </summary>
        [Required(ErrorMessage = "يجب ادخال الوضيفة ")]
        [Display(Name = "الوضيفة")]
        public string? Job { get; set; }

        /// <summary>
        /// Gets or sets the user of the account
        /// </summary>
        public UserDto User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user who created the account
        /// </summary>
        public UserDto AddedBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the permissions of the account
        /// </summary>

        [Display(Name = "القراة")]
        public string? ReadPermission { get; set; }

        [Display(Name = "الكتابة")]
        public string? WritePermission { get; set; }

        [Display(Name = "التعديل")]
        public string? UpdatePermission { get; set; }

        [Display(Name = "الحذف")]
        public string? DeletePermission { get; set; }

        public long? UserId { get; set; }

        /// <summary>
        /// Gets or set the current active status of the account
        /// </summary>

        [Display(Name = "نشط")]
        public bool IsActive { get; set; } = false;
    }
}

public enum SocialStatus
{
    Unspecified,
    Unmarried,
    Married,
    Divorced,
    Widowed,
}