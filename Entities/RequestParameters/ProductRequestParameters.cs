namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int MinPrice { get; set; } = 0; // Sqlite'da decimal desteği olmadığı için fiyatları int olarak tutuyoruz
        public int MaxPrice { get; set; } = int.MaxValue;
        public bool IsValidPrice => MaxPrice > MinPrice;
    }
}
