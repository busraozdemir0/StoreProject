namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int MinPrice { get; set; } = 0; // Sqlite'da decimal desteği olmadığı için fiyatları int olarak tutuyoruz
        public int MaxPrice { get; set; } = int.MaxValue;
        public bool IsValidPrice => MaxPrice > MinPrice;
        public int PageNumber{get;set;}  // kaç sayfa olacak
        public int PageSize{get;set;} // her sayfada kaç ürün olacak
        public ProductRequestParameters():this(1,6)
        {
            
        }
        public ProductRequestParameters(int pageNumber=1, int pageSize=6)
        {
            PageNumber=pageNumber;
            PageSize=pageSize;
        }
    }
}
