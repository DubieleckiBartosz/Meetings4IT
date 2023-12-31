﻿using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.Messaging;
using System.Reflection;

namespace Meetings4IT.Shared.Implementations.Extensions;

public static class TypeExtensions
{
    public static void RegistrationAssemblyIntegrationEvents(this Assembly assembly, IEventRegistry registry)
    {
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(IntegrationEventDecoratorAttribute));
            if (attribute == null)
            {
                continue;
            }

            var attr = (IntegrationEventDecoratorAttribute)attribute;
            var navigator = attr?.Navigator!;

            registry.Register(navigator, type);
        }
    }
}