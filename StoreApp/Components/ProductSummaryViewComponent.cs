using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummaryViewComponent:ViewComponent
    {
        private readonly IServiceManager _manager;

        public ProductSummaryViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke()  // String ile geçtiğimiz için herhangi bir view ile dönmemize gerek yoktur.
        {// Product'ların sayısını verecek
            return _manager.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
}