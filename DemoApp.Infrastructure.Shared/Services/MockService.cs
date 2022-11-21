using DemoApp.Application.Interfaces;
using DemoApp.Domain.Entities;
using DemoApp.Infrastructure.Shared.Mock;

using System.Collections.Generic;

namespace DemoApp.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {
        public List<Enrolment> GetEnrolment(int rowCount)
        {
            var positionFaker = new EnrolmentInsertBogusConfig();
            return positionFaker.Generate(rowCount);
        }

        public List<Subject> GetSubjects()
        {
            return new List<Subject> { };
        }

        public List<Student> GetStudents()
        {
            return new List<Student> { };
        }
    }
}