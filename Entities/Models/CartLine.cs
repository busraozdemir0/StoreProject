namespace Entities.Models
{
    public class CartLine // sepetteki her bir ürünü ve bu ürünün adedini tutacak
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }=new(); // tanımlandığı yerde doğrudan ürün referansı alsın
        public int Quantity{get;set;} // sepete bu üründen kaç adet eklendi
    }
}