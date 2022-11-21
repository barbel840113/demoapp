using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Wrappers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Enrolments.Commands.CreateEnrolment
{
    public partial class InsertMockEnrolmentCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedEnrolmentCommandHandler : IRequestHandler<InsertMockEnrolmentCommand, Response<int>>
    {
        private readonly ISubjectRepositoryAsync _studentRepository;

        public SeedEnrolmentCommandHandler(ISubjectRepositoryAsync positionRepository)
        {
            _studentRepository = positionRepository;
        }

        public async Task<Response<int>> Handle(InsertMockEnrolmentCommand request, CancellationToken cancellationToken)
        {
            await _studentRepository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}