using DemoApp.Application.Interfaces.Repositories;

using FluentValidation;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Subjects.Commands.CreateSubject
{
    public class CreatSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        private readonly ISubjectRepositoryAsync positionRepository;

        public CreatSubjectCommandValidator(ISubjectRepositoryAsync positionRepository)
        {
            this.positionRepository = positionRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();            
        }
    }
}