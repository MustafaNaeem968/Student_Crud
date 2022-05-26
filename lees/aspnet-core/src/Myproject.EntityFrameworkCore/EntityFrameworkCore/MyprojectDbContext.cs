using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Myproject.Authorization.Roles;
using Myproject.Authorization.Users;
using Myproject.MultiTenancy;

namespace Myproject.EntityFrameworkCore
{
    public class MyprojectDbContext : AbpZeroDbContext<Tenant, Role, User, MyprojectDbContext>
    {
        public DbSet<Students.Student> Students { get; set; }
        
        public MyprojectDbContext(DbContextOptions<MyprojectDbContext> options)
            : base(options)
        {
        }
    }
}
