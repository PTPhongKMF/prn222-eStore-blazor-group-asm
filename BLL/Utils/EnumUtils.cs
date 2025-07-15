using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BLL.Utils;

public static class EnumUtils
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? enumValue.ToString();
    }
} 