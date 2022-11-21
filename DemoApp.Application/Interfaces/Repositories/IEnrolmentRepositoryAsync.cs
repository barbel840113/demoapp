using DemoApp.Application.Features.Enrolments.Queries.GetEnrolment;
using DemoApp.Application.Parameters;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Application.Interfaces.Repositories
{
    public interface IEnrolmentRepositoryAsync : IGenericRepositoryAsync<Enrolment>
    {
        Task<IEnumerable<Enrolment>> GetAllEnrolmentByStudentId(Guid studentId, EnrolmentStatus enrolmentStatus);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEnrolmentReponseAsync(GeEnrolmentQuery requestParameters);
    }
}