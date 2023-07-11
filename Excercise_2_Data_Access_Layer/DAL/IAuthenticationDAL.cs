using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.AuthenticationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public interface IAuthenticationDAL
    {
        public User GetUser(int id);
        public Task<bool> AddUserAsync(User user);
        public Task<User> FindByEmailAsync(string email);
        public Task<bool> UpdateUserAsync(User user);
        public Task<User> GetUserByToken(string token);
    }
}
