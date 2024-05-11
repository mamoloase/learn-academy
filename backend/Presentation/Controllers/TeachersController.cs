using System.Security.Claims;

using Application.Features.Teacher.Requests;

using Domain.Constants;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
public class TeachersController : BaseController
{
    private readonly IMediator _mediator;
    public TeachersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromForm] CreateTeacherRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpPut]
    [Authorize(Roles = $"{RoleConstants.Admin} ,{RoleConstants.Teacher}")]
    public async Task<IActionResult> Update([FromForm] UpdateTeacherRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }
    [HttpDelete]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(DeleteTeacherRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] GetTeachersRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

}
