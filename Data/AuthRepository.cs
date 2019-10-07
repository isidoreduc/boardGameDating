using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BoardGameDating.api.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameDating.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;

        public AuthRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordSalt, passwordHash;
            // out allow us to pass by reference, thus not needing to initialize the variables
            CreatePasswordHash(password, out passwordSalt, out passwordHash);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        // hashes the password string received from user using a salt
        private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            // if no user found or the password is wrong
            if(user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
        }

        // compare user entered password's hash with the hash in our database
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // checking each byte of the hash against the hash in our database
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<bool> UserExists(string username) => 
            await _dbContext.Users.AnyAsync(user => user.Name == username) ?  true : false;
        
    }
}