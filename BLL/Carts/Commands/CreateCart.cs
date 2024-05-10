using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Commands;

public record CreateCartCommand : IRequest<Guid>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCartCommand, Guid>
{
    private readonly IRepository<Cart> _repository;

    public CreateCategoryCommandHandler(IRepository<Cart> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        Cart cart = new Cart();      
        return await _repository.Insert(cart);
    }
}
