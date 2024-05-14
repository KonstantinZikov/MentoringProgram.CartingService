using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Commands;

public record CreateCartCommand(string id) : IRequest;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand>
{
    private readonly IRepository<Cart, string> _repository;

    public CreateCartCommandHandler(IRepository<Cart, string> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        Cart cart = new Cart { Id = request.id };
        await _repository.Insert(cart);
    }
}
