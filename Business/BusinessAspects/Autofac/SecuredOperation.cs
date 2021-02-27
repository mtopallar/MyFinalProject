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
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //jwt ı da gönderere istek yapıldığında her istek için bir http context i (thread i) oluşur

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //rolleri virgülden bölüp array e at
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; //yani metodu çalıştırmaya devam et.
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
