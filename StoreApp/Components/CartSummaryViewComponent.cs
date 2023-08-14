using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        private readonly Cart _cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            _cart=cartService;
        }
        public string Invoke() // görünüm nesnesi oluşturmaya ihtiyaç duymadığımız için string bir şekilde geçtik (IViewComponentResult kullanmadığımız için View'ını yapmamıza gerek kalmadı)
        {
            return _cart.Lines.Count().ToString(); // karttaki ürünlerin toplam sayısı
        }
    }
}