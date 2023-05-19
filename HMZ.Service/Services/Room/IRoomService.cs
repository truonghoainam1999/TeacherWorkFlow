using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Services.Room
{
    public interface IRoomService
    {
        Task<int> GetAll (string id);
    }
}
