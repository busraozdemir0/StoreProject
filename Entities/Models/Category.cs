namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // Nesne adında Id varsa otomatik olarak ef tarafından Primary Key olarak değerlendirilir
        public String? CategoryName { get; set; }=String.Empty;
        public ICollection<Product> Products { get; set; } //Collection navigation prop, nesneler arasında gezinmemiz için
    }
}