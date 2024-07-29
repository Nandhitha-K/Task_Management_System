using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Model;

namespace TaskLibrary.Repo
{
    public interface ITaskScheduleRepo
    {
        Task<Tasks> GetSchedules(int taskid);
        Task<List<Tasks>> GetAllTaskSchedules();
        Task<List<Tasks>>GetUserById(int userid);
        Task<List<Tasks>> GetByMailId(string email);
        Task<List<Tasks>> GetByStartDate(int userid,DateTime startdate);
        Task InsertTaskSchedule(Tasks task);
        Task UpdateTaskSchedule(int taskid,Tasks task);
        Task DeleteTaskSchedule(int taskid);

    }
}