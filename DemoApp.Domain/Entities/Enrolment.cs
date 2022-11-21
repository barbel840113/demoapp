using DemoApp.Domain.Common;
using DemoApp.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Domain.Entities
{
    public class Enrolment: AuditableBaseEntity
    {        
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public EnrolmentStatus EnrolmentStatus { get; set; }
    }
}
