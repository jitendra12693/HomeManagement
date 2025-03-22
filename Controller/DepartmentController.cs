using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            var newDepartment = await _departmentRepository.AddDepartmentAsync(department);
            if (newDepartment == null)
            {
                return BadRequest("Department is already exists.");
            }


            return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, newDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (department == null || department.Id != id)
            {
                return BadRequest();
            }

            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            await _departmentRepository.UpdateDepartmentAsync(department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}