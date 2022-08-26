﻿using APSS.Web.Mvc.Auth.Requirements;

namespace APSS.Web.Mvc.Auth.Handlers;

public sealed class ShouldHaveUpdatePermissionRequirementHandler
    : ShouldHavePermissionsRequirementHandler<ShouldHaveUpdatePermissionRequirement>
{ }