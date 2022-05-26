using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Myproject.EntityFrameworkCore;
using Myproject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Myproject.Web.Tests
{
    [DependsOn(
        typeof(MyprojectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MyprojectWebTestModule : AbpModule
    {
        public MyprojectWebTestModule(MyprojectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyprojectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MyprojectWebMvcModule).Assembly);
        }
    }
}