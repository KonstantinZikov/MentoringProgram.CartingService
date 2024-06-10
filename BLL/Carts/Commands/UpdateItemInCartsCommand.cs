using Ardalis.GuardClauses;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

public record UpdateItemInCartsCommand : IRequest
{
    public required int ProductId { get; init; }

    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public string? ImageAlt { get; init; }

    public decimal Price { get; set; }

    public required string PriceCurrency { get; set; }
}

public class UpdateItemInCartsCommandHandler : IRequestHandler<UpdateItemInCartsCommand>
{
    private readonly IRepository<Cart, string> _repository;

    public UpdateItemInCartsCommandHandler(IRepository<Cart, string> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateItemInCartsCommand request, CancellationToken cancellationToken)
    {
        // LiteDB's Find function can't process complex expressions, so have to load all the carts here
        var allCarts = await _repository.List();
        IEnumerable<Cart> carts = allCarts.Where(c => c.Items.Any(i => i.ProductId == request.ProductId));

        Image? image = null;

        if (request.ImageUrl != null)
        {
            image = new Image { Url = request.ImageUrl, Alt = request.ImageAlt };
        }

        foreach (var cart in carts)
        {
            foreach (LineItem item in cart.Items.Where(i => i.ProductId == request.ProductId))
            {
                item.Name = request.Name;
                item.Image = image;
                item.Price = new Money(request.Price, request.PriceCurrency);
            }
        }

        await _repository.Update(carts);
    }
}
