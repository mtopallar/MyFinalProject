using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // IServiceCollection bizim asp.NET uygulamamızın (kısaca API mizin) servis bağımlılıklarını eklediğimiz ya da araya girmesini istediğimiz servisleri eklediğimiz koleksiyonun ta kendisidir.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
