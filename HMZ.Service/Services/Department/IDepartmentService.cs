using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Services.Department
{
    public interface IDepartmentService
    {
        Task<int> GetAll(string id);
    }
}
