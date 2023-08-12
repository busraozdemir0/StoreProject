using System.ComponentModel.DataAnnotations;


namespace Entities.Dtos
{
    public record ProductDto // record type'lar classlarla benzer yapıda olsa da arkaplandaki çalışma yapısında farklılıklar vardır.
    {
        public int ProductId { get; init; } // set olursa nesne yapısı değişebilir fakat init yazarsak nesne yapısı değişemez.

        [Required(ErrorMessage = "Product name is required")] 
        public String? ProductName { get; init; } = String.Empty;

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; init; }
        public int? CategoryId { get; init; } 
    }
}
