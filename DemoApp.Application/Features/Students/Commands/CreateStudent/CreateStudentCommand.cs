using AutoMapper;

using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Commands.CreatePosition
{
    public partial class CreateStudentCommand : IRequest<Response<Guid>>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }       
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response<Guid>>
    {
        private readonly ISubjectRepositoryAsync _studentRepository;
        private readonly IMapper _mapper;

        public CreateStudentCommandHandler(ISubjectRepositoryAsync positionRepository, IMapper mapper)
        {
            _studentRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var position = _mapper.Map<Student>(request);
            await _studentRepository.AddAsync(position);
            return new Response<Guid>(position.Id);
        }
    }
}