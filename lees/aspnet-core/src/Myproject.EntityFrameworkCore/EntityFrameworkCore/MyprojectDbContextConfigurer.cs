using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Myproject.EntityFrameworkCore
{
    public static class MyprojectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyprojectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyprojectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
