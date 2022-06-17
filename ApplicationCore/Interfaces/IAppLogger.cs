using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void Log(string message);
        void Log(string message, Exception exception);

    }
}
