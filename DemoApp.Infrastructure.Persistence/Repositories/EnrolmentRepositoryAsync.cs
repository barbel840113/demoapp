using DemoApp.Application.Features.Employees.Queries.GetEmployees;
using DemoApp.Application.Features.Enrolments.Queries.GetEnrolment;
using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;
using DemoApp.Infrastructure.Persistence.Contexts;
using DemoApp.Infrastructure.Persistence.Repository;

using LinqKit;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Persistence.Repositories
{
    public class EnrolmentRepositoryAsync : GenericRepositoryAsync<Enrolment>, IEnrolmentRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Enrolment> _employee;
        private IDataShapeHelper<Enrolment> _dataShaper;
        private readonly IMockService _mockData;

        public EnrolmentRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Enrolment> dataShaper,
            IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _employee = dbContext.Set<Enrolment>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<IEnumerable<Enrolment>> GetAllEnrolmentByStudentId(Guid studentId, EnrolmentStatus enrolmentStatus)
        {
            return await _dbContext.Enrolment.Where(x => x.StudentId == studentId && x.EnrolmentStatus == enrolmentStatus).ToListAsync();
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEnrolmentReponseAsync(GeEnrolmentQuery requestParameter)
        {
            IQueryable<Enrolment> result;

            var studentId = requestParameter.StudentId;
            var subjectId = requestParameter.SubjectId;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            int seedCount = 1000;

            result = _mockData.GetEnrolment(seedCount)
                .AsQueryable();

            // Count records total
            recordsTotal = result.Count();

            // filter data
            FilterByColumn(ref result, studentId, subjectId);

            // Count records after filter
            recordsFiltered = result.Count();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            //limit query fields
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Enrolment>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            // var resultData = await result.ToListAsync();
            // Note: Bogus library does not support await for AsQueryable.
            // Workaround:  fake await with Task.Run and use regular ToList
            var resultData = await Task.Run(() => result.ToList());

            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(ref IQueryable<Enrolment> positions, Guid studentId, Guid subjectId)
        {
            if (!positions.Any())
            {
                return;
            }
               

            if (studentId == default(Guid) && default(Guid) == subjectId)
            {
                return;
            }
               

            var predicate = PredicateBuilder.New<Enrolment>();

            if (default(Guid) != subjectId)
                predicate = predicate.And(p => p.SubjectId.Equals(subjectId));

            if (studentId != default(Guid))
                predicate = predicate.And(p => p.StudentId.Equals(studentId));

            positions = positions.Where(predicate);
        }
    }
}