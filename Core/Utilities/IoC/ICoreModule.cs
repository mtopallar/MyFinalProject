using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection); //Genel bağımlılıkları yükleyecek metodumuz.
    }
}

//** .NetCore IServiceSollection'ın ne olduğunu / kim olduğunu biliyor. Onun için ona göre enjeksiyon yapacaktır.
        
//** Business.DependencyResolvers.Autofac altında yer alan ve projemiz özelinde bağımlılıkları çözümlediğimiz AutofacBusinessModule dosyasında da Load metodu olduğunu hatırla. Oradaki Load metoduna da ContainerBuilder ı yani Autofac i dahil ettik. Burada ise .NetCore'un kendi servis bağımlılıklarının interface i olan IServiceSollection ı dahil ediyoruz.
        
//** Sonuçta ICoreModule bir interface bu interface in implementasyonunu da Core.DependencyResolvers altındaki CoreModule sınıfında gerçekleştiriyoruz.
