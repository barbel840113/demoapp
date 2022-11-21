using AutoMapper;

using DemoApp.Application.Features.Employees.Queries.GetEmployees;
using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Subjects.Queries.GetSubject
{
    public partial class PagedSubjectQuery : IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        //strong type input parameters
        public int Draw { get; set; } //page number
        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        public int Length { get; set; } //page size
        public IList<Order> Order { get; set; } //Order by
        public Search Search { get; set; } //search criteria
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PageSubjectQueryHandler : IRequestHandler<PagedSubjectQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly ISubjectRepositoryAsync _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public PageSubjectQueryHandler(ISubjectRepositoryAsync subjectRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedDataTableResponse<IEnumerable<Entity>>> Handle(PagedSubjectQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetSubjectQuery();

            // Draw map to PageNumber
            validFilter.PageNumber = (request.Start / request.Length) + 1;
            // Length map to PageSize
            validFilter.PageSize = request.Length;

            // Map order > OrderBy
            var colOrder = request.Order[0];
            switch (colOrder.Column)
            {
                case 0:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "PositionNumber" : "PositionNumber DESC";
                    break;

                case 1:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "PositionTitle" : "PositionTitle DESC";
                    break;

                case 2:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "PositionDescription" : "PositionDescription DESC";
                    break;
            }

            // Map Search > searchable columns
            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                //limit to fields in view model
                validFilter.PositionNumber = request.Search.Value;
                validFilter.PositionTitle = request.Search.Value;
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetSubjectViewModel>();
            }
            // query based on filter
            var entityPositions = await _subjectRepository.GetPagedSubjectReponseAsync(validFilter);
            var data = entityPositions.data;
            RecordsCount recordCount = entityPositions.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}