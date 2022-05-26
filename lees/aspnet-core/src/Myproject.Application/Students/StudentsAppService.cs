using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Myproject.Authorization;

namespace Myproject.Students
{
    //[AbpAuthorize(PermissionNames.Pages_students)]
    public class StudentsAppService : CrudAppService<Student, StudentDto,int, PagedResultRequestDto>
    {
       public StudentsAppService(IRepository<Student,int> repository) : base(repository)
        { }
        public async Task<List<StudentDto>> GetAllStudentList(string name)
            {
            var result = await Repository.GetAll().Where(a=> a.Name.Contains(name)||a.Email.Contains(name)||a.Degree.Contains(name)|| a.Number.Contains(name)).Select(a => new StudentDto
            {
                Id = a.Id,
                Name = a.Name,
                Degree =a.Degree,
                 Email = a.Email,
                 Number = a.Number
                  
            }).ToListAsync();
            return result;
        }
    }
    
}
