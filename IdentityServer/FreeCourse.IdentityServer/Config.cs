// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_image_stock"){Scopes = {"image_stock_fullpermission"}},
            new ApiResource("resource_basket"){Scopes = {"basket_fullpermission"}},
            new ApiResource("resource_discount"){Scopes = {"discount_fullpermission"}},
            new ApiResource("resource_order"){Scopes = {"order_fullpermission"}},
            new ApiResource("resource_payment"){Scopes = {"payment_fullpermission"}},
            new ApiResource("resource_gateway"){Scopes = {"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            
            //mikroservisleri buraya ekliyoruz.
       
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(), //Bu alan zorunludur ve Jwt'nin payload'ındaki sub claim'idir.
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name = "roles", DisplayName = "Roles", Description = "Kullanıcı rolleri", UserClaims = new[]{"role"}} 
                          //IdentityResource'lar Claim'lerle eşlenmelidir.

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("image_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope("basket_fullpermission","Basket API için full erişim"),
                new ApiScope("discount_fullpermission", "Discount API için full erişim"),
                new ApiScope("order_fullpermission", "Order API için full erişim"),
                new ApiScope("payment_fullpermission","Payment API için full erişim"),
                new ApiScope("gateway_fullpermission","Gateway API için full erişim"),

            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                  new Client
                  {
                      ClientName= "Asp.Net Core MVC",
                      ClientId = "WebMvcClient",
                      ClientSecrets= {new Secret("secret".Sha256())},
                      AllowedGrantTypes = GrantTypes.ClientCredentials,//Refresh Token yok.
                      AllowedScopes = {"catalog_fullpermission","image_stock_fullpermission,","gateway_fullpermission",IdentityServerConstants.LocalApi.ScopeName}
                  },

                   new Client
                  {
                      ClientName= "Asp.Net Core MVC",
                      ClientId = "WebMvcClientForUser",
                      AllowOfflineAccess = true,
                      ClientSecrets= {new Secret("secret".Sha256())},
                      AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                      AllowedScopes = {"basket_fullpermission","order_fullpermission","gateway_fullpermission", IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.LocalApi.ScopeName,"roles"}, //Kullanıcı login olmasa bile kullanıcı adına token(refresh token) alabiliyoruz o yüzden offline access.
                           AccessTokenLifetime = 1*60*60,//60dk
                           RefreshTokenExpiration = TokenExpiration.Absolute,
                           AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,//RefreshToken'ın ömrü 60 gün.
                           RefreshTokenUsage = TokenUsage.ReUse

                  },
                    new Client
                  {
                      ClientName= "Token Exchange Client",
                      ClientId = "TokenExchangeClient",
                      ClientSecrets= {new Secret("secret".Sha256())},
                      AllowedGrantTypes = new []{"urn:ietf:params:oauth:grant-type:token-exchange" },
                      AllowedScopes = { "discount_fullpermission", "payment_fullpermission",IdentityServerConstants.StandardScopes.OpenId}
                  }
            };
    }
}