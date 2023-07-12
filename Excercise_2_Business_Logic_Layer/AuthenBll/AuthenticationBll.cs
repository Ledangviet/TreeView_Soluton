using AutoMapper;
using Excercise_2_Data_Access_Layer.DAL;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.AuthenticationDTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.AuthenBll
{
    public class AuthenticationBll : IAuthenticationBll
    {
        private readonly IMapper _mapper;
        private IConfiguration _config;
        private readonly IAuthenticationDAL _authenDAL;
        public AuthenticationBll(IAuthenticationDAL authenDAL, IMapper mapper, IConfiguration config)
        {
            _authenDAL = authenDAL;
            _mapper = mapper;
            _config = config;
        }

        /// <summary>
        /// Sign up => check password && email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SignUpResponseModel> SignUpAsync(SignUpModel model)
        {
            try
            {
                var responseModel = new SignUpResponseModel();
                var checkuser = await _authenDAL.FindByEmailAsync(model.Email);
                if (checkuser != null)
                {
                    responseModel.Status = "Email already exist!";
                    return responseModel;
                }

                model.Password = ComputeSHA256(model.Password);
                var user = _mapper.Map<User>(model);
                var result = await _authenDAL.AddUserAsync(user);
                if (result == true)
                {
                    responseModel.Succeeded = true;
                    responseModel.Status = "SignUp Succeeded!";
                    return responseModel;
                }
                responseModel.Status = "SignUp Fail!";
                return responseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sign in by password and 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SignInResponseModel> SignInAsync(SignInModel model)
        {
            var responseModel = new SignInResponseModel();
            var user = await _authenDAL.FindByEmailAsync(model.Email);

            //user check
            if (user == null)
            {
                responseModel.Status = "Login failed!";
                return responseModel;
            }
            model.Password = ComputeSHA256(model.Password);

            //passwork check
            if (user.Password == model.Password)
            {
                responseModel.Succeeded = true;
                responseModel.Status = "SignIn succeeded!";

                var accessToken = GenerateAccessToken(user);
                //generate refresh token
                var refreshToken = GenerateRefreshToken();

                //response token model
                var tokenmodel = new TokenModel
                {
                    UserName = user.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                responseModel.Token = tokenmodel;

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(3);
                await _authenDAL.UpdateUserAsync(user);
                return responseModel;
            }
            responseModel.Status = "Password Incorrect!";
            return responseModel;
        }


        //Clear refreshtoken & expired time
        public async Task<bool> LogOutAsync(string email)
        {
            try
            {
                var user = await _authenDAL.FindByEmailAsync(email);
                if (user == null)
                {
                    return false;
                }
                user.RefreshToken = "";
                user.RefreshTokenExpiryTime = null;
                var result = _authenDAL.UpdateUserAsync(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Get new access token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetToken(TokenModel model)
        {
            try
            {
                var res = new TokenResponse();
                var user = await _authenDAL.GetUserByToken(model.RefreshToken);
                if (user == null)
                {
                    res.Status = false;
                    res.StatusMessage = "User doesn't exist";
                    return res;
                }
                if (user.RefreshTokenExpiryTime > DateTime.Now)
                {
                    var accessToken = GenerateAccessToken(user);
                    res.Status = true;
                    res.StatusMessage = "Success!";
                    res.Token = accessToken;                    
                    return res;
                }
                await LogOutAsync(user.Email);
                res.Status = false;
                res.StatusMessage = "Token expired!";
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate an accesstoken for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateAccessToken(User user)
        {
            //claim info
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid
                    ().ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:ValidIssuer"],
                audience: _config["Jwt:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
                );
            //generate token
            var tokenhandle = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenhandle;
        }

        /// <summary>
        /// generate random refresh token
        /// </summary>
        /// <returns></returns>
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string ComputeSHA256(string s)
        {
            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }
            return hash;
        }


    }
}
