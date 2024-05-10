using BLL.Common.Interfaces;
using DAL.Common.Interface;
using DAL.Entities;

namespace BLL.Providers
{
    public class CurrentCartProvider : ICurrentCartProvider
    {
        private readonly IRepository<Cart> _repository;

        public CurrentCartProvider(IRepository<Cart> repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetCurrentCart(CancellationToken cancellationToken)
        {
            Cart? cart = (await _repository.List()).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart { Id = Guid.NewGuid()};
                await _repository.Insert(cart);
            }

            return cart;
        }
    }
}
