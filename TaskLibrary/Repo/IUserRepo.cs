using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Model;

namespace TaskLibrary.Repo
{
    public interface IUserRepo
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUsersByEmail(string email);
        Task InsertUser(Users user);
        
    }
}
