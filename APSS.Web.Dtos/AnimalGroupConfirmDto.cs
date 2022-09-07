using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupConfirmDto : BaseAuditbleDto
    {
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is Required")]
        public string Type { get; set; } = null!;

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Type is Required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Sex")]
        public AnimalSex Sex { get; set; }

        [Required(ErrorMessage = "Erea Name is Required")]
        [Display(Name = "Erea Name")]
        public string UserName { get; set; } = null!;

        public long UserID { get; set; }

        /// <summary>
        /// Gets or sets the supervisor
        /// </summary>
        public User? SupervisedBy { get; set; }
    }
}