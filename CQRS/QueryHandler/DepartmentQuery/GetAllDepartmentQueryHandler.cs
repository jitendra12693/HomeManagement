using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeManagement.CQRS.QueryHandler.DepartmentQuery
{
    public class GetAllDepartmentQueryHandler:IRequestHandler<GetAllDepartmentQuery, List<Department>>
    {
        private readonly HomeManagementDbContext _context;
        public GetAllDepartmentQueryHandler(HomeManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _context.Departments.ToListAsync();
        }
    }
    public class GetDepartmentByIdQueryHandler:IRequestHandler<GetDepartmentByIdQuery, Department>
    {
        private readonly HomeManagementDbContext _context;
        public GetDepartmentByIdQueryHandler(HomeManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Department> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Departments.FindAsync(request.Id);
        }
    }
   
}