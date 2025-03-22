using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;

namespace HomeManagement.Repository.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAsync(LoginModel loginModel);
    }
}