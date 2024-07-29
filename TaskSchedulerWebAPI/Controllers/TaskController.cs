using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Model;
using TaskLibrary.Repo;

namespace TaskSchedulerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskScheduleRepo repo;
        public TaskController(ITaskScheduleRepo taskRepo)
        {
            repo = taskRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Tasks> listallschedules = await repo.GetAllTaskSchedules();
            return Ok(listallschedules);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Tasks task)
        {
            try
            {
                await repo.InsertTaskSchedule(task);
               
                return Created($"api/Task/{task.TaskId}", task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{taskid}")]
        public async Task<ActionResult> Update(int taskid, Tasks task)
        {
            try
            {
                await repo.UpdateTaskSchedule(taskid,task);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskid}")]
        public async Task<ActionResult> Delete(int taskid)
        {
            try
            {
                await repo.DeleteTaskSchedule(taskid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{taskid}")]
        public async Task<ActionResult> GetOne(int taskid)
        {
            try
            {
                Tasks tasks= await repo.GetSchedules(taskid);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("ByUserId/{userid}")]
        public async Task<ActionResult> GetByUserId(int userid)
        {
            try
            {
                List<Tasks> tasks = await repo.GetUserById(userid);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByMailId/{Email}")]
        public async Task<ActionResult> GetByMailId(string Email)
        {
            try
            {
                List<Tasks> tasks = await repo.GetByMailId(Email);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByDate/{userid}/{startDate}")]
        public async Task<ActionResult>GetByStartDate(int userid,DateTime startDate)
        {
            try
            {
                List<Tasks> task=await repo.GetByStartDate(userid,startDate);
                return Ok(task);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
