﻿using System.Text.RegularExpressions;

using APSS.Domain.Entities.Util;

namespace APSS.Domain.Entities;

public static class AccessLevelExtensions
{
    /// <summary>
    /// Checks if this access level is above the passed level
    /// </summary>
    /// <param name="self"></param>
    /// <param name="rhs">The access level to check against</param>
    /// <returns></returns>
    public static bool IsAbove(this AccessLevel self, AccessLevel rhs)
        => (uint)self > (uint)rhs;

    /// <summary>
    /// Checks if this access level is below the passed level
    /// </summary>
    /// <param name="self"></param>
    /// <param name="rhs">The access level to check against</param>
    /// <returns></returns>
    public static bool IsBelow(this AccessLevel self, AccessLevel rhs)
        => (uint)self < (uint)rhs;

    /// <summary>
    /// Checks if this access level is above or equal the passed level
    /// </summary>
    /// <param name="self"></param>
    /// <param name="rhs">The access level to check against</param>
    /// <returns></returns>
    public static bool IsAboveOrEqual(this AccessLevel self, AccessLevel rhs)
        => (uint)self >= (uint)rhs;

    /// <summary>
    /// Checks if this access level is below or equal the passed level
    /// </summary>
    /// <param name="self"></param>
    /// <param name="rhs">The access level to check against</param>
    /// <returns></returns>
    public static bool IsBelowOrEqual(this AccessLevel self, AccessLevel rhs)
        => (uint)self <= (uint)rhs;

    /// <summary>
    /// Gets the next level above this level
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static AccessLevel NextLevelUpove(this AccessLevel self)
    {
        if (self == AccessLevel.Root)
            throw new InvalidOperationException("root users cannot have superusers");

        return (AccessLevel)(((uint)self) << 1);
    }

    /// <summary>
    /// Gets the next level below this level
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static AccessLevel NextLevelBelow(this AccessLevel self)
    {
        if (self == AccessLevel.Farmer)
            throw new InvalidOperationException("famers cannot have subusers");

        return (AccessLevel)(((uint)self) >> 1);
    }

    /// <summary>
    /// Gets levels as an enumerable
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static IEnumerable<AccessLevel> AsEnumerable(this AccessLevel self)
    {
        foreach (var level in self.GetSetValues())
            yield return level;
    }
}