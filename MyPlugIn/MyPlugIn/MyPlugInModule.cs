using MyPlugIn.Contents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyPlugIn.Data;
using MyPlugIn.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Uow;
using Volo.Abp.Account;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Caching;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.Emailing;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Host;
using Volo.Abp.Gdpr;
using Volo.Abp.LeptonX.Shared;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.Security.Claims;
using MyPlugInCode;
using Volo.Abp.Data;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;

namespace MyPlugIn;

[DependsOn(
    // ABP Framework packages
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),

    // Suite templates package
    typeof(VoloAbpCommercialSuiteTemplatesModule),

    // LeptonX Theme module package
    typeof(AbpAspNetCoreMvcUiLeptonXThemeModule),

    // Account module packages
    typeof(AbpAccountPublicWebOpenIddictModule),
    typeof(AbpAccountPublicHttpApiModule),
    typeof(AbpAccountPublicApplicationModule),

    typeof(AbpAccountAdminHttpApiModule),
    typeof(AbpAccountAdminApplicationModule),

    // Identity module packages
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityProEntityFrameworkCoreModule),

    typeof(AbpOpenIddictProHttpApiModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(AbpOpenIddictProEntityFrameworkCoreModule),

    // Audit logging module packages
    typeof(AbpAuditLoggingHttpApiModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),

    // Permission Management module packages
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),

    // Saas Management module packages
    typeof(SaasHostHttpApiModule),
    typeof(SaasHostApplicationModule),
    typeof(SaasEntityFrameworkCoreModule),

    // Feature Management module packages
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),

    // Setting Management module packages
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),

    // Text Template Management module packages
    typeof(TextTemplateManagementHttpApiModule),
    typeof(TextTemplateManagementApplicationModule),
    typeof(TextTemplateManagementEntityFrameworkCoreModule),

    // Language Management module packages
    typeof(LanguageManagementHttpApiModule),
    typeof(LanguageManagementApplicationModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),

    // GDPR module packages
    typeof(AbpGdprHttpApiModule),
    typeof(AbpGdprApplicationModule),
    typeof(AbpGdprEntityFrameworkCoreModule),

    // Blob Storing
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
)]
public class MyPlugInModule : AbpModule
{
    /* Single point to enable/disable multi-tenancy */
    //private const bool IsMultiTenant = true;

    //public override void PreConfigureServices(ServiceConfigurationContext context)
    //{
    //    var hostingEnvironment = context.Services.GetHostingEnvironment();
    //    var configuration = context.Services.GetConfiguration();

    //    context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
    //    {
    //        options.AddAssemblyResource(
    //            typeof(MyPlugInResource)
    //        );
    //    });

    //    PreConfigure<OpenIddictBuilder>(builder =>
    //    {
    //        builder.AddValidation(options =>
    //        {
    //            options.AddAudiences("MyPlugIn");
    //            options.UseLocalServer();
    //            options.UseAspNetCore();
    //        });
    //    });

    //    if (!hostingEnvironment.IsDevelopment())
    //    {
    //        PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
    //        {
    //            options.AddDevelopmentEncryptionAndSigningCertificate = false;
    //        });

    //        PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
    //        {
    //            serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "83e0b85a-7e5a-45db-a316-9d5b64e72e14");
    //        });
    //    }

    //    MyPlugInGlobalFeatureConfigurator.Configure();
    //    MyPlugInModuleExtensionConfigurator.Configure();
    //}

    //public override void ConfigureServices(ServiceConfigurationContext context)
    //{
    //    var hostingEnvironment = context.Services.GetHostingEnvironment();
    //    var configuration = context.Services.GetConfiguration();

    //    if (!hostingEnvironment.IsProduction())
    //    {
    //        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
    //    }

    //    if (hostingEnvironment.IsDevelopment())
    //    {
    //        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
    //    }

    //    ConfigureAuthentication(context);
    //    ConfigureBundles();
    //    ConfigureMultiTenancy();
    //    ConfigureUrls(configuration);
    //    ConfigureAutoMapper(context);
    //    ConfigureImpersonation(context);
    //    ConfigureSwagger(context.Services, configuration);
    //    ConfigureExternalProviders(context.Services);
    //    ConfigureAutoApiControllers();
    //    ConfigureLocalization();
    //    ConfigureCors(context, configuration);
    //    ConfigureDataProtection(context);
    //    ConfigureVirtualFiles(hostingEnvironment);
    //    ConfigureEfCore(context);
    //    ConfigureTheme();
    //}

    //private void ConfigureTheme()
    //{
    //    Configure<LeptonXThemeOptions>(options =>
    //    {
    //        options.DefaultStyle = LeptonXStyleNames.System;
    //    });
    //}

    //private void ConfigureAuthentication(ServiceConfigurationContext context)
    //{
    //    context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    //    context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
    //    {
    //        options.IsDynamicClaimsEnabled = true;
    //    });
    //}

    //private void ConfigureBundles()
    //{
    //    Configure<AbpBundlingOptions>(options =>
    //    {
    //        options.StyleBundles.Configure(
    //            LeptonXThemeBundles.Styles.Global,
    //            bundle => { bundle.AddFiles("/global-styles.css"); }
    //        );
    //    });
    //}

    //private void ConfigureMultiTenancy()
    //{
    //    Configure<AbpMultiTenancyOptions>(options =>
    //    {
    //        options.IsEnabled = IsMultiTenant;
    //    });
    //}

    //private void ConfigureUrls(IConfiguration configuration)
    //{
    //    Configure<AppUrlOptions>(options =>
    //    {
    //        options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
    //        options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

    //        options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
    //        options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
    //    });
    //}

    //private void ConfigureLocalization()
    //{
    //    Configure<AbpLocalizationOptions>(options =>
    //    {
    //        options.Resources
    //            .Add<MyPlugInResource>("en")
    //            .AddBaseTypes(typeof(AbpValidationResource))
    //            .AddVirtualJson("/Localization/MyPlugIn");

    //        options.DefaultResourceType = typeof(MyPlugInResource);

    //        options.Languages.Add(new LanguageInfo("en", "en", "English"));
    //        options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
    //        options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
    //        options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
    //        options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
    //        options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
    //        options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
    //        options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
    //        options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
    //        options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
    //        options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
    //        options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
    //        options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
    //        options.Languages.Add(new LanguageInfo("ru", "ru", "Русский", "ru"));
    //        options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
    //        options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
    //        options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
    //        options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
    //        options.Languages.Add(new LanguageInfo("es", "es", "Español"));
    //    });

    //    Configure<AbpExceptionLocalizationOptions>(options =>
    //    {
    //        options.MapCodeNamespace("MyPlugIn", typeof(MyPlugInResource));
    //    });
    //}

    //private void ConfigureAutoApiControllers()
    //{
    //    Configure<AbpAspNetCoreMvcOptions>(options =>
    //    {
    //        options.ConventionalControllers.Create(typeof(MyPlugInModule).Assembly);
    //    });
    //}

    //private void ConfigureImpersonation(ServiceConfigurationContext context)
    //{
    //    context.Services.Configure<AbpAccountOptions>(options =>
    //    {
    //        options.TenantAdminUserName = "admin";
    //        options.ImpersonationTenantPermission = SaasHostPermissions.Tenants.Impersonation;
    //        options.ImpersonationUserPermission = IdentityPermissions.Users.Impersonation;
    //    });
    //}

    //private void ConfigureSwagger(IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAbpSwaggerGenWithOAuth(
    //        configuration["AuthServer:Authority"]!,
    //        new Dictionary<string, string>
    //        {
    //            {"MyPlugIn", "MyPlugIn API"}
    //        },
    //        options =>
    //        {
    //            options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyPlugIn API", Version = "v1" });
    //            options.DocInclusionPredicate((docName, description) => true);
    //            options.CustomSchemaIds(type => type.FullName);
    //        });
    //}

    //private void ConfigureExternalProviders(IServiceCollection services)
    //{
    //    services.AddAuthentication()
    //        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    //        {
    //            options.ClaimActions.MapJsonKey(AbpClaimTypes.Picture, "picture");
    //        })
    //        .WithDynamicOptions<GoogleOptions, GoogleHandler>(
    //            GoogleDefaults.AuthenticationScheme,
    //            options =>
    //            {
    //                options.WithProperty(x => x.ClientId);
    //                options.WithProperty(x => x.ClientSecret, isSecret: true);
    //            }
    //        )
    //        .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
    //        {
    //            //Personal Microsoft accounts as an example.
    //            options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
    //            options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";

    //            options.ClaimActions.MapCustomJson("picture", _ => "https://graph.microsoft.com/v1.0/me/photo/$value");
    //            options.SaveTokens = true;
    //        })
    //        .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
    //            MicrosoftAccountDefaults.AuthenticationScheme,
    //            options =>
    //            {
    //                options.WithProperty(x => x.ClientId);
    //                options.WithProperty(x => x.ClientSecret, isSecret: true);
    //            }
    //        )
    //        .AddTwitter(TwitterDefaults.AuthenticationScheme, options =>
    //        {
    //            options.ClaimActions.MapJsonKey(AbpClaimTypes.Picture, "profile_image_url_https");
    //            options.RetrieveUserDetails = true;
    //        })
    //        .WithDynamicOptions<TwitterOptions, TwitterHandler>(
    //            TwitterDefaults.AuthenticationScheme,
    //            options =>
    //            {
    //                options.WithProperty(x => x.ConsumerKey);
    //                options.WithProperty(x => x.ConsumerSecret, isSecret: true);
    //            }
    //        );
    //}

    //private void ConfigureAutoMapper(ServiceConfigurationContext context)
    //{
    //    context.Services.AddAutoMapperObjectMapper<MyPlugInModule>();
    //    Configure<AbpAutoMapperOptions>(options =>
    //    {
    //        /* Uncomment `validate: true` if you want to enable the Configuration Validation feature.
    //         * See AutoMapper's documentation to learn what it is:
    //         * https://docs.automapper.org/en/stable/Configuration-validation.html
    //         */
    //        options.AddMaps<MyPlugInModule>(/* validate: true */);
    //    });
    //}

    //private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    //{
    //    context.Services.AddCors(options =>
    //    {
    //        options.AddDefaultPolicy(builder =>
    //        {
    //            builder
    //                .WithOrigins(
    //                    configuration["App:CorsOrigins"]?
    //                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
    //                        .Select(o => o.RemovePostFix("/"))
    //                        .ToArray() ?? Array.Empty<string>()
    //                )
    //                .WithAbpExposedHeaders()
    //                .SetIsOriginAllowedToAllowWildcardSubdomains()
    //                .AllowAnyHeader()
    //                .AllowAnyMethod()
    //                .AllowCredentials();
    //        });
    //    });
    //}

    //private void ConfigureDataProtection(ServiceConfigurationContext context)
    //{
    //    context.Services.AddDataProtection().SetApplicationName("MyPlugIn");
    //}

    //private void ConfigureVirtualFiles(IWebHostEnvironment hostingEnvironment)
    //{
    //    Configure<AbpVirtualFileSystemOptions>(options =>
    //    {
    //        options.FileSets.AddEmbedded<MyPlugInModule>();
    //        if (hostingEnvironment.IsDevelopment())
    //        {
    //            /* Using physical files in development, so we don't need to recompile on changes */
    //            options.FileSets.ReplaceEmbeddedByPhysical<MyPlugInModule>(hostingEnvironment.ContentRootPath);
    //        }
    //    });
    //}

    //private void ConfigureEfCore(ServiceConfigurationContext context)
    //{
    //    context.Services.AddAbpDbContext<MyPlugInDbContext>(options =>
    //    {
    //        /* You can remove "includeAllEntities: true" to create
    //         * default repositories only for aggregate roots
    //         * Documentation: https://docs.abp.io/en/abp/latest/Entity-Framework-Core#add-default-repositories
    //         */
    //        options.AddDefaultRepositories(includeAllEntities: true);
    //        options.AddRepository<Content, Contents.EfCoreContentRepository>();

    //    });

    //    Configure<AbpDbContextOptions>(options =>
    //    {
    //        options.Configure(configurationContext =>
    //        {
    //            configurationContext.UseSqlServer();
    //        });
    //    });

    //}

    //public override void OnApplicationInitialization(ApplicationInitializationContext context)
    //{
    //    var app = context.GetApplicationBuilder();
    //    var env = context.GetEnvironment();

    //    if (env.IsDevelopment())
    //    {
    //        app.UseDeveloperExceptionPage();
    //    }

    //    app.UseAbpRequestLocalization();

    //    if (!env.IsDevelopment())
    //    {
    //        app.UseErrorPage();
    //    }

    //    app.UseCorrelationId();
    //    app.UseAbpSecurityHeaders();
    //    app.UseStaticFiles();
    //    app.UseRouting();
    //    app.UseCors();
    //    app.UseAuthentication();
    //    app.UseAbpOpenIddictValidation();

    //    if (IsMultiTenant)
    //    {
    //        app.UseMultiTenancy();
    //    }

    //    app.UseUnitOfWork();
    //    app.UseDynamicClaims();
    //    app.UseAuthorization();

    //    app.UseSwagger();
    //    app.UseAbpSwaggerUI(options =>
    //    {
    //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPlugIn API");
    //        options.OAuthClientId(context.GetConfiguration()["AuthServer:SwaggerClientId"]);
    //    });

    //    app.UseAuditing();
    //    app.UseAbpSerilogEnrichers();
    //    app.UseConfiguredEndpoints();


    //    var myService = context.ServiceProvider
    //          .GetRequiredService<MyService>();

    //    myService.Initialize();
    //}
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var myService = context.ServiceProvider
            .GetRequiredService<MyService>();

        myService.Initialize();
    }
    //private void ConfigureEfCore(ServiceConfigurationContext context)
    //{
    //    var configuration = context.Services.GetConfiguration();
    //    //context.Services.AddAbpDbContext<MyPlugInDbContext>();

    //    //Configure<AbpDbConnectionOptions>(options =>
    //    //{
    //    //    options.ConnectionStrings.Default = "......";
    //    //});


    //}

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAutoMapperObjectMapper<MyPlugInModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            /* Uncomment `validate: true` if you want to enable the Configuration Validation feature.
             * See AutoMapper's documentation to learn what it is:
             * https://docs.automapper.org/en/stable/Configuration-validation.html
             */
            options.AddMaps<MyPlugInModule>(/* validate: true */);
        });
        //context.Services.AddDbContext<MyPlugInDbContext>(options =>
        //  options.UseSqlServer(configuration.GetConnectionString("MySecondaryDb")));


        context.Services.AddAbpDbContext<MyPlugInDbContext>(options =>
        {

            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Content, Contents.EfCoreContentRepository>();

        });
        var t = configuration.GetConnectionString("MySecondaryDb");
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings["MySecondaryDb"] = configuration.GetConnectionString("MySecondaryDb");
        });


        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(configurationContext =>
            {
                configurationContext.UseSqlServer();
            });
        });




     

      
        // ConfigureEfCore(context);
    }

}