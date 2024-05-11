using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Features.Order.Requests;

namespace Presentation.Controllers;
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Filter([FromQuery] GetOrdersRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromQuery] CreateOrdersRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
}
