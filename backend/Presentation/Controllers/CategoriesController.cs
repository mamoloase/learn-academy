using Domain.Constants;
using Application.Features.Category.Requests;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

public class CategoriesController : BaseController
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(UpdateCategoryRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpDelete]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(DeleteCategoryRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> Filter()
    {
        return Ok(await _mediator.Send(new GetCategoriesRequest()));
    }

}
