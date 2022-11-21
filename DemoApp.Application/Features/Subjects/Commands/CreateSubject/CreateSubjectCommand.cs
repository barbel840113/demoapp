using AutoMapper;

using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Subjects.Commands.CreateSubject
{
    public partial class CreateSubjectCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }  
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Response<Guid>>
    {
        private readonly ISubjectRepositoryAsync _studentRepository;
        private readonly IMapper _mapper;

        public CreateSubjectCommandHandler(ISubjectRepositoryAsync positionRepository, IMapper mapper)
        {
            _studentRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = _mapper.Map<Subject>(request);
            await _studentRepository.AddAsync(subject);
            return new Response<Guid>(subject.Id);
        }
    }
}