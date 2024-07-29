using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Model;
using TaskLibrary.Repo;

namespace TaskLibrary.Repo
{
    public class TaskScheduleRepo : ITaskScheduleRepo
    {
        TaskDbContext dbContext = new TaskDbContext();
        public async Task DeleteTaskSchedule(int taskid)
        {
            try
            {
                Tasks deletetaskschedule = await GetSchedules(taskid);
                dbContext.Tasks.Remove(deletetaskschedule);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("No Task is available for the given user");
            }
        }

        public async Task<List<Tasks>> GetAllTaskSchedules()
        {
            try
            {
                List<Tasks> tasks = await dbContext.Tasks.Include(task=>task.Users).ToListAsync();
                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Tasks>> GetByMailId(string email)
        {
            try
            {
                List<Tasks> tasks = await(from es in dbContext.Tasks where es.Users.Email== email select es).ToListAsync();

                return tasks;
            }
            catch
            {

                throw new Exception("No tasks available for given email ID");
            }
        }

        public async Task<List<Tasks>> GetByStartDate(int userid, DateTime startdate)
        {
            try
            {
                List<Tasks> tasks = await(from es in dbContext.Tasks where es.UserId==userid&&es.StartDate.Value.Date==startdate.Date select es).ToListAsync();

                return tasks;
            }
            catch
            {

                throw new Exception("No tasks available for given start date");
            }

        }

        public async Task<Tasks> GetSchedules(int taskid)
        {
            try
            {
                Tasks listbytaskid = await(from u in dbContext.Tasks where u.TaskId == taskid select u).FirstAsync();
                return listbytaskid;
            }
            catch
            {
                throw new Exception("Task Id not Exists");
            }
        }

        public async Task<List<Tasks>> GetUserById(int userid)
        {
            try
            {
                List<Tasks> tasks = await (from es in dbContext.Tasks where es.UserId == userid select es).ToListAsync();

                return tasks;
            }
            catch
            {

                throw new Exception("No tasks available for given user ID");
            }
        }

        public async Task InsertTaskSchedule(Tasks task)
        {
            try
            {
                await dbContext.Tasks.AddAsync(task);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateTaskSchedule(int taskid, Tasks task)
        {
            try
            {
                Tasks updatetask = await GetSchedules(taskid);
                updatetask.TaskTitle= task.TaskTitle;
                updatetask.Description = task.Description;
                updatetask.StartDate = task.StartDate;
                updatetask.EndDate= task.EndDate;
                updatetask.StatusType = task.StatusType;
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Invalid User Id or Task Id to update");
            }
        }
    }
}
