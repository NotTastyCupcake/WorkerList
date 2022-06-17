using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Entities
{
    public abstract class BaseEntities
    {
        public virtual int Id { get; protected set; }
    }
}
