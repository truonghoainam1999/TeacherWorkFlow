using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Services.TaskWorkServices
{
    public interface ITaskWorkService : IBaseService<TaskWorkQuery, TaskWorkView, TaskWorkFilter>
    {
        Task<DataResult<TaskWorkView>> GetAll();
        Task<DataResult<TaskWorkView>> GetByCodeAsync(string taskWorkCode);
	}
}
