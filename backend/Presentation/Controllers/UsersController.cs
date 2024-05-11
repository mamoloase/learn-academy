using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Requests;
using Microsoft.AspNetCore.Authorization;
using Application.Features.User.Requests;
using Domain.Constants;

namespace Presentation.Controllers;
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpPost("[Action]")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangeUserPasswordRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(UpdateUserRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }
    [HttpGet]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Filter([FromQuery] GetUsersRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

}