using APSS.Domain.Entities;
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
        [Required(ErrorMessage = "Employe Name is Required")]
        [Display(Name = "Name")]
        public string HolderName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password hash of the user
        /// </summary>
        [Required(ErrorMessage = "Passwor is Required")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// Gets or sets the national id of the user
        /// </summary>

        [Display(Name = "Namtional Id")]
        public string? NationalId { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user
        /// </summary>

        [Display(Name = "Phoen Number ")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the social status of the user
        /// </summary>

        [Display(Name = "Social Status")]
        public SocialStatus SocialStatus { get; set; } = SocialStatus.Unspecified;

        /// <summary>
        /// Gets or sets the job of the user
        /// </summary>
        [Display(Name = "Job")]
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
        [Display(Name = "Permissions")]
        public PermissionType Permissions { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// Gets or set the current active status of the account
        /// </summary>

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = false;
    }
}