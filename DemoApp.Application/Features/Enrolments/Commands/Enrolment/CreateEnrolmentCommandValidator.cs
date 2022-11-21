using DemoApp.Application.Interfaces.Repositories;

using FluentValidation;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Enrolments.Commands.CreateEnrolment
{
    public class CreateEnrolmentCommandValidator : AbstractValidator<CreateEnrolmentCommand>
    {       

        public CreateEnrolmentCommandValidator(ISubjectRepositoryAsync positionRepository)
        {        

            RuleFor(p => p.StudentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SubjectId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }  
    }
}