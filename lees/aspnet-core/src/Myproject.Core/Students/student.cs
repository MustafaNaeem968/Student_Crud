using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Myproject.Authorization.Users;

namespace Myproject.Students
{

    public class Student : Entity<int>
    {   
        public string Name { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string Number { get; set; }

        }
}
