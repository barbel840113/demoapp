using DemoApp.Domain.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Domain.Entities
{
    public class Subject : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
