using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class UsertDetailsDto : BaseAuditbleDto
    {
        [Display(Name = "Area Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or stes the access level of the user
        /// </summary>
        [Display(Name = "Access Level")]
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the user status
        /// </summary>
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the supervisor
        /// </summary>
        [Display(Name = "Area Status")]
        public User? SupervisedBy { get; set; }

        /// <summary>
        /// Gets or sets the collection of subusers under this user
        /// </summary>
        [Display(Name = "Sub Users")]
        public ICollection<User> SubUsers { get; set; } = new List<User>();

        /// <summary>
        /// Gets or sets the collection of accounts under this user
        /// </summary>
        [Display(Name = "User Accounts")]
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}