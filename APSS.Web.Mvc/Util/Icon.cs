using Microsoft.AspNetCore.Html;

namespace APSS.Web.Mvc.Util;

public enum IconSize
{
    Default,
    UltraSmall,
    ExtraSmall,
    Small,
    Large,
    ExtraLarge,
    UltraLarge,
}

public enum Icon
{
    None,
    Home,
    Users,
    Key,
    Poll,
    Cow,
    Mountain,
    Wheat,
    Gear,
    Link,
    ChevronUp,
    ChevronRight,
    ChevronDown,
    ChevronLeft,
    Person,
    People,
    Scale,
    Ruler,
    Cheese,
    Seeding,
    Calendar,
    Plus,
    Pen,
    TrashCan,
    List,
}

public static class IconExtensoins
{
    private static readonly Dictionary<IconSize, string> _faIconSizes = new()
    {
        { IconSize.Default, string.Empty },
        { IconSize.UltraSmall, "fa-2xs" },
        { IconSize.ExtraSmall, "fa-xs" },
        { IconSize.Small, "fa-sm" },
        { IconSize.Large, "fa-lg" },
        { IconSize.ExtraLarge, "fa-xl" },
        { IconSize.UltraLarge, "fa-2xl" },
    };

    private static readonly Dictionary<Icon, string> _faIcons = new()
    {
        { Icon.Home, "fa-solid fa-house" },
        { Icon.Users, "fa-solid fa-users" },
        { Icon.Key, "fa-solid fa-key" },
        { Icon.Poll, "fa-solid fa-square-poll-horizontal" },
        { Icon.Cow, "fa-solid fa-cow" },
        { Icon.Mountain, "fa-solid fa-mountain-sun" },
        { Icon.Wheat, "fa-solid fa-wheat-awn" },
        { Icon.Gear, "fa-solid fa-gear" },
        { Icon.Link, "fa-solid fa-link" },
        { Icon.ChevronUp, "fa-solid fa-chevron-up" },
        { Icon.ChevronRight, "fa-solid fa-chevron-right" },
        { Icon.ChevronDown, "fa-solid fa-chevron-down" },
        { Icon.ChevronLeft, "fa-solid fa-chevron-left" },
        { Icon.People, "fa-solid fa-people-group" },
        { Icon.Person, "fa-solid fa-person" },
        { Icon.Scale, "fa-solid fa-scale-balanced" },
        { Icon.Ruler, "fa-solid fa-ruler" },
        { Icon.Cheese, "fa-solid fa-cheese" },
        { Icon.Seeding, "fa-solid fa-seeding" },
        { Icon.Calendar, "fa-regular fa-calendar" },
        { Icon.Plus, "fa-solid fa-plus" },
        { Icon.Pen, "fa-solid fa-pen" },
        { Icon.TrashCan, "fa-solid fa-trash-can" },
        { Icon.List, "fa-solid fa-list-ul" },
    };

    public static HtmlString? Render(
        this Icon icon,
        IconSize size = IconSize.Default,
        string additionalClasses = "")
    {
        if (icon == Icon.None)
            return null;

        var iconClass = _faIcons[icon];
        var sizeClass = _faIconSizes[size];

        return new($"<i class=\"{sizeClass} {iconClass} {additionalClasses}\"></i>");
    }
}