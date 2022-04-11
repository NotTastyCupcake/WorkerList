using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Entities
{
    public class PersonItem : BaseEntities
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public PositionItem Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public DateTime? DateOfDismissal { get; set; }
    }
}
