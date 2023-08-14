using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace StoreApp.Pages
{
    public class CartModel:PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; } //IoC

        public CartModel(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            Cart = cart;
        }

        public string ReturnUrl { get; set; }
        public void OnGet (string returnUrl) // Sepete giderken hangi sayfadan geldiyse o sayfanın url'ini tutacağız. Sepetten geri çıktığında tekrar o sayfaya döndürebilmek için
        {
            ReturnUrl = returnUrl ?? "/"; // referans almadıysa / kullanarak kök dizine gidecek
        }

        public IActionResult OnPost(int productId, string returnUrl) // Sepete ürün ekleme işlemi
        {
            Product? product=_manager.ProductService.GetOneProduct(productId,false);
            if(product is not null)
            {
                Cart.AddItem(product,1); // her eklemede bir tane ürün ekliyor
            }
            return Page(); //returnUrl
        }

        public IActionResult OnPostRemove(int id, string returnUrl) // Sepetten ürün kaldırma işlemi
        {
            Cart.RemoveLine(Cart.Lines.First(x=>x.Product.ProductId.Equals(id)).Product);
            return Page();
        }
    }
}