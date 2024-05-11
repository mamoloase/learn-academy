using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain.Constants;
using Application.Features.Course.Requests;

namespace Presentation.Controllers;
public class CoursesController : BaseController
{
    private readonly IMediator _mediator;
    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] GetCoursesRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> Comments([FromQuery] GetCourseCommentsRequest request)
    {
        return Ok(await _mediator.Send(request));
    }


    [HttpGet("[Action]")]
    public async Task<IActionResult> Get([FromQuery] GetCourseRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("[Action]")]
    [Authorize]
    public async Task<IActionResult> Purchased([FromQuery] GetPurchasedCourseRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpPost]
    [Authorize(Roles = RoleConstants.Teacher)]
    public async Task<IActionResult> Create([FromForm] CreateCourseRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }

    [HttpPost("[Action]")]
    [Authorize]
    public async Task<IActionResult> AddComment(CreateCourseCommentRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }

    [HttpPost("[Action]")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteComment(DeleteCourseCommentRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    [Authorize(Roles = $"{RoleConstants.Teacher} ,{RoleConstants.Admin}")]
    public async Task<IActionResult> Update([FromForm] UpdateCourseRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }
    [HttpDelete]
    [Authorize(Roles = $"{RoleConstants.Teacher} ,{RoleConstants.Admin}")]
    public async Task<IActionResult> Delete([FromForm] DeleteCourseRequest request)
    {
        request.User = User;

        return Ok(await _mediator.Send(request));
    }
}
