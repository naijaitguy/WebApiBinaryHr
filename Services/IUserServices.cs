using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public interface IUserServices
    {

        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(string ID);

        public Task<RefreshToken> AuthenticateRefreshToken(string token, string ipAddress);
        public Task<bool> RevokeToken(string token, string ipAddress);
        public Task<User> GetUserByRefreshToken(string refreshtoken);

        public void UpdateUserByToken(User Model);


    }
}
