using System.Threading.Tasks;
using Myproject.Configuration.Dto;

namespace Myproject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
