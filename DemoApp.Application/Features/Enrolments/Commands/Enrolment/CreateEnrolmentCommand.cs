using AutoMapper;

using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Enrolments.Commands.CreateEnrolment
{
    public partial class CreateEnrolmentCommand : IRequest<Response<Guid>>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }       
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateEnrolmentCommand, Response<Guid>>
    {
        private readonly IEnrolmentRepositoryAsync _enrolmentRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateStudentCommandHandler(
            IEnrolmentRepositoryAsync enrolmentRepository, 
            IMapper mapper,
            IEmailService emailService)
        {
            _enrolmentRepository = enrolmentRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateEnrolmentCommand request, CancellationToken cancellationToken)
        {
            // check if the student or subject id exist.
            var fetchEnroledSubject = await  _enrolmentRepository.GetAllEnrolmentByStudentId(request.StudentId, EnrolmentStatus.INCOMPLETED);
            if (fetchEnroledSubject.Count() >= 5)
            {
                return new Response<Guid>("User has more than 5 active courses.");
            }
            var enrolment = _mapper.Map<Enrolment>(request);
            await _enrolmentRepository.AddAsync(enrolment);

            if(enrolment.Id != default)
            {
                // TODO Implement and send email to user
                await _emailService.SendAsync(new DTOs.Email.EmailRequest { });
            }
            return new Response<Guid>(enrolment.Id);
        }
    }
}