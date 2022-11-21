using DemoApp.Application.Features.Employees.Queries.GetEmployees;
using DemoApp.Application.Parameters;
using DemoApp.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Application.Interfaces.Repositories
{
    public interface ISubjectRepositoryAsync : IGenericRepositoryAsync<Subject>
    {
        Task<bool> SeedDataAsync(int rowCount);
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedSubjectReponseAsync(GetSubjectQuery requestParameter);
    }
}