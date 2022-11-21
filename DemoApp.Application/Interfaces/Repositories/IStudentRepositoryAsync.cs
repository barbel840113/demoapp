using DemoApp.Application.Features.Students.Queries.GetPositions;
using DemoApp.Application.Parameters;
using DemoApp.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Application.Interfaces.Repositories
{
    public interface IStudentRepositoryAsync : IGenericRepositoryAsync<Student>
    {

        Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedStudentReponseAsync(GetStudentsQuery requestParameters);
    }
}