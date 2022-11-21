using DemoApp.Application.Features.Positions.Queries.GetPositions;
using DemoApp.Application.Features.Students.Queries.GetPositions;
using DemoApp.Application.Interfaces;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Application.Parameters;
using DemoApp.Domain.Entities;
using DemoApp.Infrastructure.Persistence.Contexts;
using DemoApp.Infrastructure.Persistence.Repository;

using LinqKit;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Persistence.Repositories
{
    public class StudentRepositoryAsync : GenericRepositoryAsync<Student>, IStudentRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Student> _students;
        private IDataShapeHelper<Student> _dataShaper;
        private readonly IMockService _mockData;

        public StudentRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Student> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _students = dbContext.Set<Student>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }
             

        public async Task<bool> SeedDataAsync(int rowCount)
        {
            foreach (Student position in _mockData.GetStudents())
            {
                await this.AddAsync(position);
            }
            return true;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedStudentReponseAsync(GetStudentsQuery requestParameter)
        {
            var positionNumber = requestParameter.PositionNumber;
            var positionTitle = requestParameter.PositionTitle;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _students
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, positionNumber, positionTitle);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

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

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Student>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(ref IQueryable<Student> positions, string positionNumber, string positionTitle)
        {
            if (!positions.Any())
                return;

            if (string.IsNullOrEmpty(positionTitle) && string.IsNullOrEmpty(positionNumber))
                return;

            var predicate = PredicateBuilder.New<Student>();

            if (!string.IsNullOrEmpty(positionNumber))
                predicate = predicate.Or(p => p.PositionNumber.Contains(positionNumber.Trim()));

            if (!string.IsNullOrEmpty(positionTitle))
                predicate = predicate.Or(p => p.PositionTitle.Contains(positionTitle.Trim()));

            positions = positions.Where(predicate);
        }       
    }
}