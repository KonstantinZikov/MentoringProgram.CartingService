using AutoMapper;
using BLL.Common.Interfaces;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Queries;

public record GetCartQuery : IRequest<CartDto>;

public class GetCategoryQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly ICurrentCartProvider _currentCartProvider;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IRepository<Cart> repository, ICurrentCartProvider cartProvider, IMapper mapper)
    {
        _currentCartProvider = cartProvider;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Cart cart = await _currentCartProvider.GetCurrentCart(cancellationToken);
        return _mapper.Map<CartDto>(cart);
    }
}
