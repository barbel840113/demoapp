using Bogus;

using DemoApp.Domain.Entities;

using System;

namespace DemoApp.Infrastructure.Shared.Mock
{
    public class EnrolmentInsertBogusConfig : Faker<Enrolment>
    {
        public EnrolmentInsertBogusConfig()
        {
            // TODO Implement full
            RuleFor(o => o.StudentId, f => Guid.NewGuid());           
        }
    }
}