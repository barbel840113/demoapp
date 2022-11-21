using DemoApp.Application.Exceptions;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Commands.UpdatePosition
{
    public class UpdateStudentCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class UpdatePositionCommandHandler : IRequestHandler<UpdateStudentCommand, Response<Guid>>
        {
            private readonly ISubjectRepositoryAsync _studentRepository;

            public UpdatePositionCommandHandler(ISubjectRepositoryAsync positionRepository)
            {
                _studentRepository = positionRepository;
            }

            public async Task<Response<Guid>> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.GetByIdAsync(command.Id);

                if (student == null)
                {
                    throw new ApiException($"Student Not Found.");
                }
                else
                {
                    student.FirstName = command.FirstName;
                    student.LastName = command.LastName;
                    student.Email = command.Email;
                    await _studentRepository.UpdateAsync(student);
                    return new Response<Guid>(student.Id);
                }
            }
        }
    }
}