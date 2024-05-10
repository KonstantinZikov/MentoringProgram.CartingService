using Ardalis.GuardClauses;
using BLL.Common.Interfaces;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

public record RemoveItemFromCartCommand(Guid Id) : IRequest;

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand>
{
    private readonly IRepository<Cart> _repository;
    private readonly ICurrentCartProvider _cartProvider;

    public RemoveItemFromCartCommandHandler(IRepository<Cart> repository, ICurrentCartProvider cartProvider)
    {
        _repository = repository;
        _cartProvider = cartProvider;
    }

    public async Task Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        Cart cart = await _cartProvider.GetCurrentCart(cancellationToken);

        LineItem? itemToRemove = cart.Items.FirstOrDefault(i => i.Id == request.Id);

        Guard.Against.NotFound(request.Id, itemToRemove);

        cart.Items.Remove(itemToRemove);
        await _repository.Update(cart);
    }
}
