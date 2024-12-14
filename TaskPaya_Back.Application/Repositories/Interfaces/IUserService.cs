using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Entities.Users;

namespace TaskPaya_Back.Application.Repositories.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(long Id);
        Task<List<User>> GetAllCustomers();
        Task<long> CreateUser(string fullname, string address);
    }
}
