using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using MediatR;

namespace HomeManagement.CQRS.CommandHandler.DepartmentCommandHandler
{
    public record CreateDepartmentCommand(string Name,string Description,bool IsActive) : IRequest<Department>;
    public record UpdateDepartmentCommand(string Name,string Description,bool IsActive,int Id) : IRequest<Department>;
    public record DeleteDepartmentCommand(int Id) : IRequest<bool>;
}