using AutoMapper;

using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Enrolments.Queries.GetEnrolment
{
    /// <summary>
    /// GetAllEmployeesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GeEnrolmentQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        //examples:
        public Guid StudentId { get; set; }

        public Guid SubjectId { get; set; }

        public EnrolmentStatus EnrolmentStatus {get;set;}
    }

    public class GetEnrolmentQueryHandler : IRequestHandler<GeEnrolmentQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IEnrolmentRepositoryAsync _enrolmentRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetEnrolmentQueryHandler(IEnrolmentRepositoryAsync enrolmentRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _enrolmentRepository = enrolmentRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GeEnrolmentQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetEnrolmentViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetEnrolmentViewModel>();
            }
            // query based on filter
            var entityEmployees = await _enrolmentRepository.GetPagedEnrolmentReponseAsync(validFilter);
            var data = entityEmployees.data;
            RecordsCount recordCount = entityEmployees.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}