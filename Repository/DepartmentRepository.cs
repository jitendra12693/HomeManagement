using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace HomeManagement.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HomeManagementDbContext _context;

        public DepartmentRepository(HomeManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            if(_context.Departments.Any(d => d.Name == department.Name))
            {
                return null;
            }
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}