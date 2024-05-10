using BLL.Carts.Commands;
using BLL.Carts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    
    public class CartController : ControllerBase
    {
        private readonly ISender _sender;

        public CartController(ILogger<CartController> logger, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("api/cart")]
        public async Task<CartDto> GetCart() => await _sender.Send(new GetCartQuery());

        [HttpPost]
        [Route("api/cart/lines")]
        public async Task<Guid> AddCategory(AddItemToCartCommand command) => await _sender.Send(command);

        [HttpDelete]
        [Route("api/cart/lines")]
        public async Task AddCategory(RemoveItemFromCartCommand command) => await _sender.Send(command);
    }
}
