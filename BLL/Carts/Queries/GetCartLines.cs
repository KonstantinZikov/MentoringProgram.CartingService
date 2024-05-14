using Ardalis.GuardClauses;
using AutoMapper;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Queries;

public record GetCartLinesQuery(string Id) : IRequest<IReadOnlyCollection<LineItemDto>>;

public class GetCartLinesQueryHandler : IRequestHandler<GetCartLinesQuery, IReadOnlyCollection<LineItemDto>>
{
    private readonly IRepository<Cart,string> _repository;
    private readonly IMapper _mapper;

    public GetCartLinesQueryHandler(IRepository<Cart,string> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<LineItemDto>> Handle(GetCartLinesQuery request, CancellationToken cancellationToken)
    {
        Cart? cart = await _repository.GetById(request.Id);

        Guard.Against.NotFound(request.Id, cart);

        return _mapper.Map<List<LineItemDto>>(cart.Items);
    }
}
