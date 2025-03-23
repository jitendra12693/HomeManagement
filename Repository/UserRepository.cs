using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeManagementDbContext _context;

        public UserRepository(HomeManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(x=>x.Department).ToListAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            var department = await _context.Departments.FindAsync(user.DepartmentId);
            if(department == null)
            {
                throw new Exception("Department not found");
            }
            if(_context.Users.Any(u => u.Email == user.Email))
            {
                return null;
            }
            user.SetPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}