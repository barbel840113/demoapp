using DemoApp.Application.Parameters;
using DemoApp.Application.Wrappers;
using DemoApp.Domain.Entities;
using DemoApp.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Features.Enrolments.Queries.GetEnrolment
{
    public class GetEnrolmentByStudentIdQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public Guid StudentId { get; set; }
        public EnrolmentStatus EnrolmentStatus;
    }
}
