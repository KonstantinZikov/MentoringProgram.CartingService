using Ardalis.GuardClauses;
using AutoMapper;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Queries;

public record GetCartQuery(string Id) : IRequest<CartDto?>;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto?>
{
    private readonly IRepository<Cart,string> _repository;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(IRepository<Cart,string> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartDto?> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Cart? cart = await _repository.GetById(request.Id);
        return _mapper.Map<CartDto>(cart);
    }
}
