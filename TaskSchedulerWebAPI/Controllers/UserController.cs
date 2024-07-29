using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Model;
using TaskLibrary.Repo;

namespace TaskSchedulerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepo repo;
        public UserController(IUserRepo userRepo)
        {
            repo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserDetails()
        {
            List<Users> users = await repo.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult> GetOne(string email)
        {
            try
            {
                Users user = await repo.GetUsersByEmail(email);
                return Ok(user);
            }
            catch (Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
            }

         [HttpPost]
         public async Task<ActionResult> Insert(Users user)
         {
             try
             {
                await repo.InsertUser(user);
                return Created($"api/User/{user.Email}", user);
             }
             catch (Exception ex)
             {
                return BadRequest(ex.Message);

             }
         }

    }
}