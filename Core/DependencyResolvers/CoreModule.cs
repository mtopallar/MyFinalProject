using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
   public class CoreModule:ICoreModule //burada proje bağımsız bağımlılıklarımızı çözüyoruz.Proje bağımlı olan bağımlılıklar business altındaki DependencyResolvers klasörü içinde çözüldü.
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); //IMemoryCache injection ının (yani MemoryCacheManager sınıfının içindeki IMemoryCache _memoryCache in) çalışabilmesi için gerekli. IMemoryCache de buradaki servicesCollection.AddMemoryCache de Microsoftun kendi sınıfları. Yani startup dan direk injection yapabiliriz ancak aspect olarak yazdığımız için API den daha geride yapıyoruz injectionlarımızı. serviceCollection.AddMemoryCache() Microsoft'un built in cache manajerinin devreye alınması için gerekli.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>(); //kronometre enjeksiyonu.
        }
    }
}
