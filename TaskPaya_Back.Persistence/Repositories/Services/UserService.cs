
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Application.Repositories.Interfaces;
using TaskPaya_Back.Application.Repositories;
using TaskPaya_Back.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace TaskPaya_Back.Persistence.Repositories.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<User> userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
       
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();
        }
        public void Dispose()
        {
            userRepository?.Dispose();
        }

        public async Task<User> GetUserById(long Id)
        {
            return await userRepository.GetEntityById(Id);
        }
        public async Task<List<User>> GetAllCustomers()
        {

            var user = await userRepository.GetEntitiesQuery().Include(s => s.Orders.Where(x => x.isPay && !x.IsDeleted)).OrderByDescending(x => x.Orders.Count()).ToListAsync();
            //var customers = userRoleRepository.GetEntitiesQuery().AsQueryable().Include(s => s.User).Where(a => a.UserId == user.Id && a.Role.Name == "Admin");
            return user;
        }

        public async Task<long> CreateUser(string fullname, string address)
        {
            var user = new User()
            {
                FullName = fullname,
                Address = address,
            };
            await userRepository.AddEntity(user);
            await userRepository.SaveChanges();
            return user.Id;
        }
    }
}
