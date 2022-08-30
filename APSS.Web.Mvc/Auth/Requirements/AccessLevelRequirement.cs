using Microsoft.AspNetCore.Authorization;

using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class AccessLevelRequirement : IAuthorizationRequirement
{
    private readonly AccessLevel _accessLevel;

    public AccessLevelRequirement(AccessLevel accessLevel)
        => _accessLevel = accessLevel;

    public AccessLevel AccessLevel => _accessLevel;
}