using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using APSS.Web.Mvc.Util.Navigation;

namespace APSS.Web.Mvc.Util;

public static class Dialog
{
    public static async Task<IHtmlContent> DeleteDialogAsync<T>(this IHtmlHelper<T> self)
        => await self.PartialAsync("Dialogs/Delete", (self.ViewContext.HttpContext.GetCurrentRoute() as CrudRoute)!);
}