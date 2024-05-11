
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Requests;

namespace Presentation.Controllers;
public class AuthController : BaseController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}
