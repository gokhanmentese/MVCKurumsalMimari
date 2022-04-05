using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public ActionResult<List<Department>> GetAll()
        {
            return _departmentService.GetAll();
        }

        [HttpGet("directorship/{id}")]
        public ActionResult<List<Department>> GetByDirectorhipId(int id)
        {
            return _departmentService.GetByDirectorhipId(id);
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetById(int id)
        {
            return _departmentService.GetById(id);

        }

        [HttpGet("byname/{name}")]
        public ActionResult<Department> GetByName(string name)
        {
            return _departmentService.GetByName(name);
        }
    }
}
