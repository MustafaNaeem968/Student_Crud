using System.Threading.Tasks;
using Abp.Application.Services;
using Myproject.Authorization.Accounts.Dto;

namespace Myproject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
