using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Model;
namespace TaskSchedulerMVCApp.Controllers
{
    public class UserController : Controller
    {
        static HttpClient svc = new HttpClient { BaseAddress = new Uri("http://localhost:5123/api/User/") };
        public async Task<ActionResult> Index()
        {  
            List<Users> reg = await svc.GetFromJsonAsync<List<Users>>("");
            return View(reg);
        }
       

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Verify(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await svc.GetAsync($"ByEmail/{email}");
                if (ModelState.IsValid)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Users reg = await response.Content.ReadFromJsonAsync<Users>();

                        if (reg.Email == email && reg.Password == password)
                        {
                            TempData["UserId"] = reg.UserId;
                            TempData["Login"] = true;
                            HttpContext.Session.SetInt32("UserId", reg.UserId);
                            HttpContext.Session.SetString("UserName", reg.UserName);
                            HttpContext.Session.SetString("Email", reg.Email);
                            HttpContext.Session.SetString("IsLogin", "true");
                            return RedirectToAction("Index", "Task");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User doesn't exists");
                    }
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");   
        }
       
        public ActionResult Register()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Users user)
        {
            try
            {
                HttpResponseMessage res = await svc.GetAsync($"ByEmail/{user.Email}");
                if (res.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("Email", "Email already exists. Please choose a different one.");
                    return View("Register", user);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View("Register", user);
                    }
                    await svc.PostAsJsonAsync<Users>("", user);
                    TempData["Register"] = true;
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                return View();
            }
        }

        
    }
}