using AutoMapper;

using DemoApp.Application.Features.Enrolments.Commands.CreateEnrolment;
using DemoApp.Application.Features.Enrolments.Queries.GetEnrolment;
using DemoApp.Application.Features.Students.Commands.CreatePosition;
using DemoApp.Application.Features.Students.Queries.GetPositions;
using DemoApp.Application.Features.Subjects.Commands.CreateSubject;
using DemoApp.Application.Features.Subjects.Queries.GetSubject;
using DemoApp.Domain.Entities;

namespace DemoApp.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Student, GetStudentsViewModel>().ReverseMap();
            CreateMap<Enrolment, GetEnrolmentViewModel>().ReverseMap();
            CreateMap<Subject, GetSubjectQuery>().ReverseMap();
            CreateMap<CreateEnrolmentCommand, Enrolment>();
            CreateMap<CreateStudentCommand, Student>();
            CreateMap<CreateSubjectCommand, Subject>();
        }
    }
}