using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ShowcaseViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(string page = "default")
        {
            /* Vitrindeki ürünleri Admin Dashboard'da resimsiz bir şekilde listelemek istediğimiz için Showcase 
            altına List adında bi view görünümü daha oluşturarak farklı bir sayfa tasarımı yaptık.
            Aşağıdaki ifadede de eğer sayfa defaulta eşitse o gelsin fakat değilse de List sayfası gelsin gibi bi mantık işlettik. */
            var products = _manager.ProductService.GetShowcaseProducts(false);
            return page.Equals("default") ? View(products) : View("List", products);  
        }
    }
}
