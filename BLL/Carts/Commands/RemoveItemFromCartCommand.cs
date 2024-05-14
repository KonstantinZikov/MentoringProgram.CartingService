using Ardalis.GuardClauses;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

public record RemoveItemFromCartCommand : IRequest
{ 
    public required string CartId { get; set; }

    public required int LineItemId { get; set; }
}


public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand>
{
    private readonly IRepository<Cart, string> _repository;

    public RemoveItemFromCartCommandHandler(IRepository<Cart, string> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _repository.GetById(request.CartId);

        Guard.Against.NotFound(request.CartId, cart);

        LineItem? itemToRemove = cart.Items.FirstOrDefault(i => i.Id == request.LineItemId);

        Guard.Against.NotFound(request.LineItemId, itemToRemove);

        cart.Items.Remove(itemToRemove);
        await _repository.Update(cart);
    }
}
