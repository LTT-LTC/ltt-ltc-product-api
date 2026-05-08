using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using LTC.ProductService.EntityFrameworkCore;
using LTC.ProductService.MultiTenancy;
using LTC.Shared.Hosting.Microservices;
using LTC.Shared.Hosting.Microservices.Authentication;
using LTC.Shared.Hosting.Microservices.MultiTenancy;
using LTC.Shared.Hosting.Microservices.OpenApi.Swagger;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(ProductServiceApplicationModule),
    typeof(ProductServiceEntityFrameworkCoreModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(LTCSharedHostingMicroservicesModule)
    )]
public class ProductServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<Volo.Abp.Security.Claims.AbpClaimsPrincipalFactoryOptions>(options => { options.IsDynamicClaimsEnabled = false; });
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        Configure<AbpAspNetCoreMultiTenancyOptions>(options =>
        {
            options.TenantKey = "X-Tenant";
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<ProductServiceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}src{0}LTC.ProductService.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ProductServiceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}src{0}LTC.ProductService.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ProductServiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}src{0}LTC.ProductService.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ProductServiceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}src{0}LTC.ProductService.Application", Path.DirectorySeparatorChar)));
            });
        }

        context.ConfigureSwaggerServices("LTC Product Service API Endpoint", "v1");

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "Ø§Ù„Ø¹Ø±Ø¨ÙŠØ©"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "ÄŒeÅ¡tina"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "FranÃ§ais"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "PortuguÃªs"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "RomÃ¢nÄƒ"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Ð ÑƒÑÑÐºÐ¸Ð¹"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "TÃ¼rkÃ§e"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "ç®€ä½“ä¸­æ–‡"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "ç¹é«”ä¸­æ–‡"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
            options.Languages.Add(new LanguageInfo("es", "es", "EspaÃ±ol"));
            options.Languages.Add(new LanguageInfo("el", "el", "Î•Î»Î»Î·Î½Î¹ÎºÎ¬"));
        });



        context.ConfigureAuthenticationJwtBearer();

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "ProductService:";
        });

        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = false;
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("ProductService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "ProductService-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        context.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(LTC.Shared.Hosting.Microservices.ApplicationExceptionFilterAttribute));
            options.Filters.Add(typeof(TenantValidationFilter));
        });

        ConfigureCloudinary(context);
    }

    private void ConfigureCloudinary(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<Cloudinary>(provider =>
        {
            var configuration = context.Services.GetConfiguration();
            var cloudName = configuration["CloudinarySettings:CloudName"];
            var apiKey = configuration["CloudinarySettings:ApiKey"];
            var apiSecret = configuration["CloudinarySettings:ApiSecret"];

            var account = new Account(cloudName, apiKey, apiSecret);
            var cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            return cloudinary;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        if (context.GetConfiguration().GetValue("App:UseHttpsRedirection", true))
        {
            app.UseHttpsRedirection();
        }
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        string swaggerRoutePrefix = "ltc/product-service/swagger";
        app.UseConfiguredSwagger("LTC Product Service", swaggerRoutePrefix);
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}








