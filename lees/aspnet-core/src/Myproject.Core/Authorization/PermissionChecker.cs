using Abp.Authorization;
using Myproject.Authorization.Roles;
using Myproject.Authorization.Users;

namespace Myproject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
