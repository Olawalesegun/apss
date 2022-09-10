using System.ComponentModel.DataAnnotations;

using APSS.Web.Dtos.ValueTypes;

namespace APSS.Web.Dtos
{
    public class AccountDto : BaseAuditbleDto
    {
        /// <summary>
        /// Gets or sets the name of the peron who holds this account
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string HolderName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the national id of the user
        /// </summary>
        [Display(Name = "Namtional Id")]
        public string? NationalId { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user
        /// </summary>
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the social status of the user
        /// </summary>
        [Display(Name = "Social Status")]
        public SocialStatusDto SocialStatus { get; set; }

        /// <summary>
        /// Gets or sets the job of the user
        /// </summary>
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
        public PermissionTypeDto Permissions { get; set; } = new PermissionTypeDto();

        /// <summary>
        /// Gets or set the current active status of the account
        /// </summary>
        [Display(Name = "Enabled")]
        public bool IsActive { get; set; } = false;
    }
}