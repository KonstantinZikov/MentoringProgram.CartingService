using BLL.Carts.Commands;

namespace BLL.Services
{
    public interface ICartService
    {
        Task<int> AddLineToCartWithCartCreation(AddItemToCartCommand command);
    }
}
