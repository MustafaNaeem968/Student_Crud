using System.ComponentModel.DataAnnotations;

namespace Myproject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}