using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Subjects.Commands.CreateSubject
{
    public partial class InsertMockSubjectCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedSubjectCommandHandler : IRequestHandler<InsertMockSubjectCommand, Response<int>>
    {
        private readonly ISubjectRepositoryAsync _subjectRepository;

        public SeedSubjectCommandHandler(ISubjectRepositoryAsync positionRepository)
        {
            _subjectRepository = positionRepository;
        }

        public async Task<Response<int>> Handle(InsertMockSubjectCommand request, CancellationToken cancellationToken)
        {
            await _subjectRepository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}