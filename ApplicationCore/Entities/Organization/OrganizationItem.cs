using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Entities.Organization
{
    public class OrganizationItem : BaseEntities
    {
        public string Name { get; private set; }

        public string Address { get; private set; }

        public OrganizationItem(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
