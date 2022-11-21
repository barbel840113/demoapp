using DemoApp.Application.Interfaces.Repositories;

using FluentValidation;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Commands.CreatePosition
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly ISubjectRepositoryAsync positionRepository;

        public CreateStudentCommandValidator(ISubjectRepositoryAsync positionRepository)
        {
            this.positionRepository = positionRepository;

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
            .EmailAddress().WithMessage("{PropertyName} is not email address.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        }       
    }
}