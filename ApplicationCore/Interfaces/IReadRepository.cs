using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
        
    }
}
