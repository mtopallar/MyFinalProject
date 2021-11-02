using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }
    }
}
//ReflectedType name space demek + FullName dersen namespace + base in (biz hep interface kullandığımız için interface in adı demek.) Örneğin IProductService.
//Arguments = metodun (varsa) parametreleri demek
//invocation.returnvalue demek metodu çalıştırma (yani veritabanına hiç gitme) ve cache managerdan getir demek (tabi cache de varsa kontrolü yapıldıktan sonra) Aslında tam karşılığı aspectin üzerinde olduüu metodun return değeri = cache den getirilecek değer olsun demek.
//invocation.proceed demek methodu çalıştır demek. Bu kısımda veri tabanından ilgili veri getirilecek.
//_cacheManager.Add(key,invocationReturnValue,_duration) proceed ile gelen veriyi ilgili key ve duration ile  cache de kaydetme işlemini yapacak.
//public override void Intercept(IInvocation invocation) metodu aslında namespace+base adı+metod adı+varsa metod parametreleri şeklinde bir key değeri oluşturuyor.Sonrasında aynı key e sahip değer cache de varsa direk oradan getir, yoksa veritabanından getir ve cache de ekle işini yapıyor tam olarak.

//var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})"; null değilse ve stringe çevrilebiliyorsa bu kısmı ekle ?? yoksa bunu ekle demek. arguments.select parametreleri seçer ve bir liste oluşturur, string.Join ise parametreleri virgülle ayırarak bir listeye dönüştürür.