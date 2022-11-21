using AutoMapper;

using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Subjects.Queries.GetSubject
{
    /// <summary>
    /// GetAllEmployeesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetSubjectQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string PositionNumber { get; set; }
        public string PositionTitle { get; set; }
    }

    public class GetAllSubjectsQueryHandler : IRequestHandler<GetSubjectQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ISubjectRepositoryAsync _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllSubjectsQueryHandler(ISubjectRepositoryAsync employeeRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _subjectRepository = employeeRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetSubjectViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetSubjectViewModel>();
            }
            // query based on filter
            var entityEmployees = await _subjectRepository.GetPagedSubjectReponseAsync(validFilter);
            var data = entityEmployees.data;
            RecordsCount recordCount = entityEmployees.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}