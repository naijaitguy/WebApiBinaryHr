using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBinaryHr.Entities;

namespace WebApiBinaryHr.Services
{
    public class UserServices:IUserServices
    {

        private BinaryHrDbContext Db;
        public UserServices(BinaryHrDbContext _db) => this.Db = _db;

        public async Task<List<User>> GetAllUsers() {

            return await this.Db.Users.ToListAsync();
        
        }
        public async Task<User> GetUserById(string ID) {

           // Guid UserId = new Guid(ID);

            return await this.Db.Users.FindAsync(ID);
        }

        public async Task<RefreshToken> AuthenticateRefreshToken(string token, string ipAddress)
        {
            var user = await this.Db.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) { return null; }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) { return null; } else { return refreshToken; }


        }


        public async Task<User> GetUserByRefreshToken(string refreshtoken)
        {

            return await this.Db.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshtoken));
        }


        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await this.Db.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            user.RefreshTokens.Remove(refreshToken);
            // revoke token and save
            //  refreshToken.Revoked = DateTime.UtcNow;
            //  refreshToken.RevokedByIp = ipAddress;
            this.Db.Update(user);
            await this.Db.SaveChangesAsync();

            return true;
        }


        public void UpdateUserByToken(User Model)
        {

            this.Db.Users.Update(Model);
        }
    }
}
