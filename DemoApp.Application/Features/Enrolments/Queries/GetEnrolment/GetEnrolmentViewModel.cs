using DemoApp.Domain.Enums;

using System;

namespace DemoApp.Application.Features.Enrolments.Queries.GetEnrolment
{
    public class GetEnrolmentViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public EnrolmentStatus EnrolmentStatus { get; set; }       
    }
}