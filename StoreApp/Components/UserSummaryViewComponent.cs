using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class UserSummaryViewComponent:ViewComponent
    {
        private readonly IServiceManager _manager;
        public UserSummaryViewComponent(IServiceManager manager)
        {
            _manager=manager;
        }
        public string Invoke() // Sayfa tasarımı yapmamıza gerek olmadığı için string olarak geçiyoruz
        {
            return _manager.AuthService.GetAllUsers().Count().ToString();
        }
    }
}