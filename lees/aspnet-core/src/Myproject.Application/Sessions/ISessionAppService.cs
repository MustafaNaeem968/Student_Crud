using System.Threading.Tasks;
using Abp.Application.Services;
using Myproject.Sessions.Dto;

namespace Myproject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
