using AutoMapper;

using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Students.Queries.GetPositions
{
    public class GetStudentsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string PositionNumber { get; set; }
        public string PositionTitle { get; set; }
    }

    public class GetAllStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ISubjectRepositoryAsync _studentRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllStudentsQueryHandler(ISubjectRepositoryAsync positionRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _studentRepository = positionRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            var pagination = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetStudentsViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetStudentsViewModel>();
            }
            // query based on filter
            var entityStudents = await _studentRepository.GetPagedStudentReponseAsync(validFilter);
            var data = entityStudents.data;
            RecordsCount recordCount = entityStudents.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}