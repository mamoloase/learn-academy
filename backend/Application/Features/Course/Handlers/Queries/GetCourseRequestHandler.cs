using MediatR;
using AutoMapper;

using Domain.Models;
using Domain.Models.Responses;

using Application.Interfaces;
using Application.Features.Course.Requests;
using Application.Exceptions;
using Domain.Constants;

namespace Application.Features.Course.Handlers.Queries;
public class GetCourseRequestHandler : IRequestHandler<GetCourseRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetCourseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetCourseRequest request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Course.GetAsync(x => x.Id == request.Id);

        if (course == null)
            throw new NotFoundException(string.Format(
                MessageConstants.ExceptionNotFound, nameof(request.Id)));

        return new Response(true)
        {
            Result = _mapper.Map<CourseModel>(course)
        };
    }
}
