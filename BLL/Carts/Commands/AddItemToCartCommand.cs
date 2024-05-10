using BLL.Common.Interfaces;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

public record AddItemToCartCommand : IRequest<Guid>
{
    public required int ProductId { get; init; }

    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public string? ImageAlt { get; init; }

    public decimal Price { get; set; }

    public required string PriceCurrency { get; set; }

    public required int Quantity { get; init; }
}

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Guid>
{
    private readonly IRepository<Cart> _repository;
    private readonly ICurrentCartProvider _cartProvider;

    public AddItemToCartCommandHandler(IRepository<Cart> repository, ICurrentCartProvider cartProvider)
    {
        _repository = repository;
        _cartProvider = cartProvider;
    }

    public async Task<Guid> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Cart cart = await _cartProvider.GetCurrentCart(cancellationToken);

        Image? image = null;

        if (request.ImageUrl != null)
        {
            image = new Image { Url = request.ImageUrl, Alt = request.ImageAlt };
        }

        var lineItem = new LineItem
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            Name = request.Name,
            Quantity = request.Quantity,
            Image = image,
            Price = new Money(request.Price, request.PriceCurrency)
        };

        cart.Items.Add(lineItem);

        await _repository.Update(cart);

        return lineItem.Id;
    }
}
