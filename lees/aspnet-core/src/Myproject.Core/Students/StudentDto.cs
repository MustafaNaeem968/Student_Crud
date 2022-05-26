using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Myproject.Students
{
    //[AutoMapFrom(typeof(Student))]
    [AutoMapTo(typeof(Student)), AutoMapFrom(typeof(Student))]
    public class StudentDto : EntityDto<int>
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string Number { get; set; }
    }
}
