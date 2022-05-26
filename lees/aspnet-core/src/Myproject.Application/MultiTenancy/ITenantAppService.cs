using Abp.Application.Services;
using Myproject.MultiTenancy.Dto;

namespace Myproject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

