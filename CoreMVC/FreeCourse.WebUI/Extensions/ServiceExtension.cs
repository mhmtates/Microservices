using FreeCourse.Web.Services;
using FreeCourse.WebUI.Handlers;
using FreeCourse.WebUI.Models.Settings;
using FreeCourse.WebUI.Services.Abstract;
using FreeCourse.WebUI.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FreeCourse.WebUI.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IIdentityService,IdentityService>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            services.AddHttpClient<IUserService,UserService>(opt =>
              {
                  opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);

              }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


            services.AddHttpClient<ICatalogService,CatalogService>(opt =>
              {
                  opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
              }).AddHttpMessageHandler<ClientCredentialTokenHandler>();


            services.AddHttpClient<IImageStockService,ImageStockService>(opt =>
              {
                  opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.ImageStock.Path}");
              }).AddHttpMessageHandler<ClientCredentialTokenHandler>();


            services.AddHttpClient<IBasketService,BasketService>(opt =>
              {
                  opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
              }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService,DiscountService>(opt =>
                  {
                      opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
                  }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IPaymentService,PaymentService>(opt =>
             {
                 opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.FakePayment.Path}");
             }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderService, OrderService>(opt =>
                 {
                     opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
                 }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
