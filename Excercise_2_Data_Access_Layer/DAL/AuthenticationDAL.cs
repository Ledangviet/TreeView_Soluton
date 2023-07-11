using Excercise_2_Data_Access_Layer.Data.DbContexts;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public class AuthenticationDAL : IAuthenticationDAL
    {
        private readonly NodeDbContext _dbContext;
        public AuthenticationDAL(NodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                var result = await _dbContext.SaveChangesAsync();
                if (result == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return null;
                }
                else return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByToken(string token)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.RefreshToken == token);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                var result = await _dbContext.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
