﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Attributes
{
    public class ServiceAttribute : Attribute
    {
        public Type InterfaceType { get; }

        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
    }
}
