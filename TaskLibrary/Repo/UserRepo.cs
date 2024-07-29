using TaskLibrary.Model;
using TaskLibrary.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Repo
{
    public class UserRepo : IUserRepo
    {
        TaskDbContext dbContext = new TaskDbContext();
   
        public async Task<List<Users>> GetAllUsers()
        {
            try
            {
                List<Users> users = await dbContext.Users.ToListAsync<Users>();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Users> GetUsersByEmail(string email)
        {
            try
            {
                Users user = await (from u in dbContext.Users where u.Email == email select u).FirstAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Email not Exists");
            }
        }
        public async Task InsertUser(Users user)
            {
            try
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
    }
}
