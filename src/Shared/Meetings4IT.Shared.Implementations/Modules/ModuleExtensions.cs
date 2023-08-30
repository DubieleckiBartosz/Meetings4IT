﻿using System.Reflection;

namespace Meetings4IT.Shared.Implementations.Modules;

public static class ModuleExtensions
{
    public static string ReadModuleName(this Type targetType)
    {
        var assembly = Assembly.GetAssembly(targetType);
        var assemblyName = assembly!.GetName();

        var parts = assemblyName.Name!.Split('.');
        var shortName = parts[0];

        return shortName;
    }
}