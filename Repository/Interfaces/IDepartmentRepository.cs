using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;

namespace HomeManagement.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}