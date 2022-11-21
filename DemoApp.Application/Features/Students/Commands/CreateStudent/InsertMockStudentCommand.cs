using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Commands.CreatePosition
{
    public partial class InsertMockStudentCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedStudentCommandHandler : IRequestHandler<InsertMockStudentCommand, Response<int>>
    {
        private readonly ISubjectRepositoryAsync _studentRepository;

        public SeedStudentCommandHandler(ISubjectRepositoryAsync positionRepository)
        {
            _studentRepository = positionRepository;
        }

        public async Task<Response<int>> Handle(InsertMockStudentCommand request, CancellationToken cancellationToken)
        {
            await _studentRepository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}