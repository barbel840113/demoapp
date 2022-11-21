using DemoApp.Domain.Enums;

using System;

namespace DemoApp.Application.Features.Subjects.Queries.GetSubject
{
    public class GetSubjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}