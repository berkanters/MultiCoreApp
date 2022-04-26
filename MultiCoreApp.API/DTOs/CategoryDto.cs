using System.ComponentModel.DataAnnotations;

namespace MultiCoreApp.API.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        public string Name { get; set; }
    }
}
