using DemoApp.Application.Exceptions;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Queries.GetPositionById
{
    public class GetStudentByIdQuery : IRequest<Response<Student>>
    {
        public Guid Id { get; set; }

        public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<Student>>
        {
            private readonly ISubjectRepositoryAsync _studentRepository;

            public GetStudentByIdQueryHandler(ISubjectRepositoryAsync positionRepository)
            {
                _studentRepository = positionRepository;
            }

            public async Task<Response<Student>> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.GetByIdAsync(query.Id);
                if (student == null)
                {
                    throw new ApiException($"Position Not Found.");
                }
                return new Response<Student>(student);
            }
        }
    }
}