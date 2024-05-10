using DAL.Entities;

namespace BLL.Common.Interfaces
{
    public interface ICurrentCartProvider
    {
        Task<Cart> GetCurrentCart(CancellationToken cancellationToken);
    }
}
