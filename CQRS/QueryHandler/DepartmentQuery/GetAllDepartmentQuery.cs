using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using MediatR;

namespace HomeManagement.CQRS.QueryHandler.DepartmentQuery
{
    public record GetAllDepartmentQuery:IRequest<List<Department>>;
    
}