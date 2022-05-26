using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Myproject.Authorization;

namespace Myproject
{
    [DependsOn(
        typeof(MyprojectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MyprojectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyprojectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyprojectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
