using Application.Features.Episode.Requests;
using Application.Features.Teacher.Requests;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
public class EpisodesController : BaseController
{
    private readonly IMediator _mediator;
    public EpisodesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery] GetEpisodesRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpGet("[Action]")]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] GetEpisodeRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpPost]
    [Authorize(Roles = RoleConstants.Teacher)]
    public async Task<IActionResult> Create([FromForm] CreateEpisodeRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpPut]
    [Authorize(Roles = $"{RoleConstants.Teacher} ,{RoleConstants.Admin}")]
    public async Task<IActionResult> Update([FromForm] UpdateEpisodeRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
    [HttpDelete]
    [Authorize(Roles = $"{RoleConstants.Teacher} ,{RoleConstants.Admin}")]
    public async Task<IActionResult> Delete([FromForm] DeleteEpisodeRequest request)
    {
        request.User = User;
        return Ok(await _mediator.Send(request));
    }
}
