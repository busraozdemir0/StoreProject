using System.Text.Json.Serialization;
using Entities.Models;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Models
{
    public class SessionCart:Cart
    {
        [JsonIgnore]
        public ISession? Session { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session=services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;

            SessionCart cart=session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            cart.Session = session;  // cart ile session bilgisini bağladık
            return cart;
        }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson<SessionCart>("cart",this);
        }
        public override void Clear() // Cart modelinde virtual şeklinde yazdığımız için bu classta override ederek üzerine yazıyoruz
        {
            base.Clear();
            Session?.Remove("cart"); // ilgili ifadeyi Session'dan kaldırdık
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session?.SetJson<SessionCart>("cart",this);
        }
    }
}