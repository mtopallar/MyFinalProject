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

/* 

 ** Bu Extensin ile ICoreModelu interfece ini implemente eden tüm servis koleksiyonlarımızı .NetCore a  eklemiş oluyoruz. Yani API nin startup ı yerine ihtiyacımız olan genel bağımlılıkları daha geri karafta core katmanımızda yönetmiş oluyoruz. Tabi ki API ye bı sınıfı kullanması gerektiğini yine Startıp dosyasında söyleyeceğiz. Ancak olur da API değişmesi ya da projeye yeni bir API eklenmesi durumunda onun Startup dosyasına da bu modülü kullanacağını söylersek tüm servisleri yeni API ye de eklemiş olacağız. Yani bundan sonra bu projedeki tüm API lere services.Add diye genel bağımlılıkları tek tek eklemek yerine,

services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
});

gibi tek bir çağırı ile ICoreModule ü implemente eden tüm sınıflardaki tüm servis bağımlılıklarımızı API ye eklemiş olacağız.

*/