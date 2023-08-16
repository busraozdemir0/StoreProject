namespace StoreApp.Models{
    public class Pagination
    {
       public int TotalItems { get; set; } // kaç tane ürün listelendiği bilgisi
       public int ItemsPerPage { get; set; } // sayfa başına düşen kayıt sayısı
       public int CurrentPage { get; set; } 
       public int TotalPages=>(int)Math.Ceiling((decimal)TotalItems/ItemsPerPage); 
    }

}