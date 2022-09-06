using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public sealed class UpdateLandForm : AddLandForm
{
    public long Id { get; set; }
}