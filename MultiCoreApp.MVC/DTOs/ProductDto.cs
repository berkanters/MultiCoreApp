using System.ComponentModel.DataAnnotations;

namespace MultiCoreApp.MVC.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} Alanı Zorunludur")]
        public string Name { get; set; }
        [Range(1,double.MaxValue, ErrorMessage = "{0} alanı 0'dan büyük olmalı")]
        public int Stock { get; set; }
        [Range(1,double.MaxValue, ErrorMessage = "{0} alanı 0'dan büyük olmalı")]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
