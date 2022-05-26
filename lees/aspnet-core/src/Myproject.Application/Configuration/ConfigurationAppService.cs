using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Myproject.Configuration.Dto;

namespace Myproject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyprojectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
