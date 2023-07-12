using Excercise_2_Data_Access_Layer.DAL;
using Excercise_2_Data_Transfer_Object.AuthenticationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.AuthenBll
{
    
    public interface IAuthenticationBll
    {
        public Task<SignInResponseModel> SignInAsync(SignInModel model);
        public Task<SignUpResponseModel> SignUpAsync(SignUpModel model);
        public Task<TokenResponse> GetToken(TokenModel model);
        public Task<bool> LogOutAsync(string email);
    }
}
