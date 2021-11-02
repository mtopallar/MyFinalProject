using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    //Authorization aspect leri genellikle business a yazılır. Çünkü projeye göre yetki lontrolü değişebilir.
    //SecuredOperation JWT için.
    // Bu class Authorization Aspect kullanabilmek için oluşturduğumuz bir class. Authorization aspectleri genellikle business içinde yer alır. Çünkü her projenin yetkilendirme algoritması değişebilir. Altyapıyı Core içine yazıyoruz ama aspect kısmını genellikle Business içine yerleştiriyoruz.
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //İçinde JWT da olan her bir istekte (istek zarfında) her istek için bir http context i (thread i) oluşur. httpContextAccessor bir isteğin gelmesi ile oluşur ve response yani cevap verilip istek tamamlanana kadar ilgili isteği takip eder.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //aspect içinde belirttiğimiz rolleri virgül ile bölüp array formatına getiriyoruz.
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //JWT zincirin içinde olduğu için injection ile IConfiguration ı enjekte edip çalıştırabilmiştik. Ancak Aspect zincirin dışında olduğu için, bu satırla Autofac ile oluşturduğumuz servis mimarisine ulaş ve karşılığı al demiş oluyor. Örneğin elimizde productService olsa ve bir winform uygulası yazıyor olsak (dependency injection imkanımız olmasa)                                         productService = ServiceTool.ServiceProvider.GetService<IProdductService>();                    gibi IoC deki karşılığı alabilir ve kullanabiliriz. Provider olarak Autofac i verdiğimiz için Autofac deki karşılıkları alacaktır. (WebAPI deki Program.cs deki .UseServiceProviderFactory(new AutofacServiceProviderFactory())) 

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //ClaimsPrincipalExtension dan faydalanarak JWT içindeki rolleri alıyoruz.
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; //aspectin bulunduğu metodu (mesela add) çalıştırmaya devam et.
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
