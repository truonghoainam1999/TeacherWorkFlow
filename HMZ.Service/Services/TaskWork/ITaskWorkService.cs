using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Services.TaskWork
{
    public interface ITaskWorkService 
    {
        Task<int> GetAll(string taskWorkId);
    }
}
