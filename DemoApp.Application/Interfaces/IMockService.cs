using DemoApp.Domain.Entities;

using System.Collections.Generic;

namespace DemoApp.Application.Interfaces
{
    public interface IMockService
    {
        List<Enrolment> GetEnrolment(int rowCount);

        List<Subject> GetSubjects();

        List<Student> GetStudents();
    }
}