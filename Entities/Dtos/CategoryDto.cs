using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record CategoryDto 
    {
        public int CategoryId { get; init; }
        [Required(ErrorMessage = "Category name is required")] 
        public string? CategoryName { get; init; }=String.Empty;
    }
}