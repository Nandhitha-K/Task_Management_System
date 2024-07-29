using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;
using TaskLibrary.Model;
using TaskSchedulerMVCApp.Models;
using TaskSchedulerMVCApp.Notification;
namespace TaskSchedulerMVCApp.Controllers
{
    public class TaskController : Controller
    {
        private IHubContext<NotificationHub> _hubContext;

        public TaskController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        static readonly HttpClient svc = new HttpClient { BaseAddress = new Uri("http://localhost:5123/api/Task/") };

        public async Task<ActionResult> Index()
        {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                int userid = ViewBag.UserId;
                List<Tasks> tasks = await svc.GetFromJsonAsync<List<Tasks>>("" + "ByUserId/" + userid);
                return View(tasks);
        }

        public async Task<ActionResult> Details(int taskid)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Tasks tasks = await svc.GetFromJsonAsync<Tasks>($"{taskid}");
            return View(tasks);
        }

        public ActionResult Create(Tasks task)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            task.UserId = HttpContext.Session.GetInt32("UserId").Value;
            ViewBag.UserId = TempData["UserId"];
            return View(task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostCreate(Tasks tasks)
        {
            try
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                await svc.PostAsJsonAsync<Tasks>("", tasks);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Task/Edit/{taskid}")]
        public async Task<ActionResult> Edit(int taskid)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Tasks tasks = await svc.GetFromJsonAsync<Tasks>($"{taskid}");
            return View(tasks);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Task/Edit/{taskid}")]
        public async Task<ActionResult> Edit(int taskid,Tasks tasks)
        {
            try
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                await svc.PutAsJsonAsync<Tasks>($"{taskid}", tasks);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Task/Delete/{taskid}")]
        public async Task<ActionResult> Delete( int taskid)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Tasks tasks = await svc.GetFromJsonAsync<Tasks>($"{taskid}");
            return View(tasks);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Task/Delete/{taskid}")]
        public async Task< ActionResult> Delete(int taskid, IFormCollection collection)
        {
            try
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                await svc.DeleteAsync($"{taskid}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NoNotification()
        {
            return View("NoNotification");
        }

        //public async Task<ActionResult> Notification(DateTime startdate)
        //{
        //    try
        //    {
        //        DateTime reminderTime = startdate.AddMinutes(-30);
        //        DateTime currentDateTime = DateTime.Now;

        //        if (reminderTime == currentDateTime)
        //        {
        //            // Send notification to clients
        //            await _hubContext.Clients.All.SendAsync("SendNotification", "Task is about to start!");
        //            return View("Notification");
        //        }
        //        else
        //        {
        //            return RedirectToAction("NoNotification", "Task");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("NoNotification", "Task");
        //    }
        //}

        [HttpGet]
        public ActionResult GetStartDateAndUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetStartDateAndUser(GetDate getDateandUser)
        {
            string dateanduser = getDateandUser.StartDate.ToString();
            return RedirectToAction("GetByStartDateAndUser", "Task", new { startDate = getDateandUser.StartDate.ToString()});
        }

        public async Task<ActionResult> GetByStartDateAndUser( string startDate)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            int userid = ViewBag.UserId;
            DateTime dateFormat = DateTime.Parse(startDate);
            string formattedDate = dateFormat.ToString("yyyy-MM-dd");
            List<Tasks> tasks = await svc.GetFromJsonAsync<List<Tasks>>("ByDate/"+userid+"/"+formattedDate);
            return View(tasks);
        }
    }
}
