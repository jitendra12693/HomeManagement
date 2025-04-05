using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.CQRS.CommandHandler.DepartmentCommandHandler;
using HomeManagement.CQRS.QueryHandler.DepartmentQuery;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISender _mediator;
        public DepartmentController(IDepartmentRepository departmentRepository, ISender mediator)
        {
            _mediator = mediator;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _mediator.Send(new GetAllDepartmentQuery());
            // var departments = await _departmentRepository.GetAllDepartmentsAsync();
             return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            //var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            var department = await _mediator.Send(new GetDepartmentByIdQuery(id));
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var department = await _mediator.Send(command);
            if (department == null)
            {
                return BadRequest("Department is already exists.");
            }   
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            // var newDepartment = await _departmentRepository.AddDepartmentAsync(department);
            // if (newDepartment == null)
            // {
            //     return BadRequest("Department is already exists.");
            // }


            // return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, newDepartment);
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