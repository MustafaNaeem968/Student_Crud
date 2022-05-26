using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Myproject.Controllers
{
    public abstract class MyprojectControllerBase: AbpController
    {
        protected MyprojectControllerBase()
        {
            LocalizationSourceName = MyprojectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
