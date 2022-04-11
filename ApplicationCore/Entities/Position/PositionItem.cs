using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Entities.Position
{
    public class PositionItem : BaseEntities
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public OrganizationItem Organization { get; set; }
        public List<PersonItem> People { get; set; }

    }
}
