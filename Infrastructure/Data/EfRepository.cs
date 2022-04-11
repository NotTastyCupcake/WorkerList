using Ardalis.Specification.EntityFrameworkCore;
using NotTastyCupcake.WorkerList.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T: class
    {
        public EfRepository(PersonContext dbContext) : base(dbContext)
        {

        }
    }
}
