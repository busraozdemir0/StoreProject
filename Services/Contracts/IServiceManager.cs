namespace Services.Contracts
{
    public interface IServiceManager
    {
        //Servisleri yönettiğimiz yer
        IProductService ProductService{ get; }
        ICategoryService CategoryService{ get; }
        IOrderService OrderService{ get; }
        IAuthService AuthService{ get; }
    }
}