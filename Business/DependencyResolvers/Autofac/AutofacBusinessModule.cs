using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CargoCompanyManager>().As<ICargoCompanyService>();
            builder.RegisterType<EfCargoCompanyDal>().As<ICargoCompanyDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();

            builder.RegisterType<CreditCardManager>().As<ICreditCardService>().SingleInstance(); ;
            builder.RegisterType<EfCreditCardDal>().As<ICreditCardDal>().SingleInstance(); ;

            builder.RegisterType<CustomerCreditCardManager>().As<ICustomerCreditCardService>().SingleInstance(); ;
            builder.RegisterType<EfCustomerCreditCardDal>().As<ICustomerCreditCardDal>().SingleInstance(); ;

            builder.RegisterType<PaymentManager>().As<IPaymentService>().SingleInstance(); ;
            builder.RegisterType<EfPaymentDal>().As<IPaymentDal>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<ProductImageManager>().As<IProductImageService>();
            builder.RegisterType<EfProductImageDal>().As<IProductImageDal>();

            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


        }
    }
}
