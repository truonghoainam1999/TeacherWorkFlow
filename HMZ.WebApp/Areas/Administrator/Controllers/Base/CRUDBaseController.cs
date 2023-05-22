
using HMZ.DTOs.Queries.Base;
using HMZ.Service.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers.Base
{
    // T: Service
    // T1: Query
    // T2: View
    // T3: Filter

    public class CRUDBaseControlle<T,T1,T2,T3> : Controller
    {
        protected readonly T _service;
        public CRUDBaseControlle(T service)
        {
            _service = service;
        }
        #region CRUD
        //  POST: Base
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery<T3> queryFilter)
        {
			queryFilter.PageNumber = queryFilter.PageNumber > 0 ? queryFilter.PageNumber : 1;
			queryFilter.PageSize = queryFilter.PageSize > 0 ? queryFilter.PageSize : 10;

            var getMethod = _service.GetType().GetMethod("GetPageList");
            if (getMethod == null)
            {
                return BadRequest("GetPageList method not found");
            }
            var data = await (Task<DataResult<T2>>)getMethod.Invoke(_service, new object[] { queryFilter });

            return data.Items == null ? BadRequest(data) : Ok(data);
        }
        // POST: Base/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T1 query)
        {
            try
            {
				var method = _service.GetType().GetMethod("CreateAsync");
				if (method == null)
				{
					return BadRequest("CreateAsync method not found");
				}
				var result = await (Task<DataResult<bool>>)method.Invoke(_service, new object[] { query });
				return result.Entity ? Ok(result) : BadRequest(result);
			}
            catch (Exception ex)
            {

				return BadRequest(ex.Message) ;
            }
            
        }

        [HttpPost]
        // POST: Base/Delete/5
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            var method = _service.GetType().GetMethod("DeleteAsync");
            if (method == null)
            {
                return BadRequest("DeleteAsync method not found");
            }
            var result = await (Task<DataResult<int>>)method.Invoke(_service, new object[] { id });
            return result.Entity > 0  ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        // POST: Base/Update/5
        public async Task<IActionResult> Update([FromBody] T1 query)
        {
            var method = _service.GetType().GetMethod("UpdateAsync");
            if (method == null)
            {
                return BadRequest("UpdateAsync method not found");
            }
            var result = await (Task<DataResult<int>>)method.Invoke(_service, new object[] { query });
            return result.Entity > 0 ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        // POST: Base/GetByIdAync/5
        public async Task<IActionResult> GetById(string id)
        {
            var method = _service.GetType().GetMethod("GetByIdAsync");
            if (method == null)
            {
                return BadRequest("GetByIdAsync method not found");
            }
            var result = await (Task<DataResult<T2>>)method.Invoke(_service, new object[] { id });

            return result.Entity == null ? BadRequest(result) : Ok(result);
        }
        [HttpPost]
        // POST: Base/GetByCodeAsync/5
        public async Task<IActionResult> GetByCode(string code)
        {
            var method = _service.GetType().GetMethod("GetByCodeAsync");
            if (method == null)
            {
                return BadRequest("GetByIdAsync method not found");
            }
            var result = await (Task<DataResult<T2>>)method.Invoke(_service, new object[] { code });

            return result.Entity == null ? BadRequest(result) : Ok(result);
        }
        #endregion

    }
}