using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using APSS.Web.Mvc.Util.Navigation;

namespace APSS.Web.Mvc.Util;

public static class Dialog
{
    public static async Task<IHtmlContent> DeleteDialogAsync<T>(this IHtmlHelper<T> self)
    {
        var route = self.ViewContext.HttpContext.GetCurrentRoute()!;

        if (route is not CrudRoute)
            route = route.Parent!;

        return await self.PartialAsync("Dialogs/Delete", (CrudRoute)route);
    }
}