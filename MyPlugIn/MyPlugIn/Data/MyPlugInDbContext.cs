using MyPlugIn.Contents;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.Data;

namespace MyPlugIn.Data;


[ConnectionStringName("MySecondaryDb")]

public class MyPlugInDbContext : AbpDbContext<MyPlugInDbContext>
{
    public DbSet<Content> Contents { get; set; } = null!;
    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public MyPlugInDbContext(DbContextOptions<MyPlugInDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        /* Include modules to your migration db context */

        //builder.ConfigureSettingManagement();
        //builder.ConfigureBackgroundJobs();
        //builder.ConfigureAuditLogging();
        //builder.ConfigureIdentityPro();
        //builder.ConfigureOpenIddictPro();
        //builder.ConfigureFeatureManagement();
        //builder.ConfigurePermissionManagement();
        //builder.ConfigureLanguageManagement();
        //builder.ConfigureSaas();
        //builder.ConfigureTextTemplateManagement();
        //builder.ConfigureBlobStoring();
        //builder.ConfigureGdpr();

        /* Configure your own entities here */
        if (builder.IsHostDatabase())
        {
            builder.Entity<Content>(b =>
            {
                b.ToTable(DbTablePrefix + "Contents", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Content.Name));
                b.Property(x => x.Value).HasColumnName(nameof(Content.Value));
            });

        }
    }
}