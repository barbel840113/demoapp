using DemoApp.Domain.Enums;

using System;

namespace DemoApp.Application.Features.Students.Queries.GetPositions
{
    public class GetStudentsViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
    }
}