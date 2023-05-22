﻿using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Services.DepartmentServices;
using HMZ.WebApp.Areas.Administrator.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HMZ.WebApp.Areas.Administrator.Controllers
{
    public class DepartmentController : CRUDBaseControlle<IDepartmentService, DepartmentQuery, DepartmentView, DepartmentFilter>
    {
        public DepartmentController(IDepartmentService service) : base(service)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] DepartmentQuery query)
        {
            return await base.Update(query);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            return await base.Delete(id);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(string id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> GetByCode(string code)
        {
            return await base.GetByCode(code);
        }
    }
}
