using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using MediatR;

namespace HomeManagement.CQRS.CommandHandler.DepartmentCommandHandler
{
    public class CreateDepartmentCommandHandler:IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly HomeManagementDbContext _context;
        public CreateDepartmentCommandHandler(HomeManagementDbContext context)
        {
            _context = context;
        }
       
        public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };
            if(_context.Departments.Any(d => d.Name == department.Name))
            {
                return null;
            }
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Id = request.Id
            };
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }
    
}