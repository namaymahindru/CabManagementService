using CabManagementService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System.Diagnostics;



namespace CabManagementService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();

        }

       
        public IActionResult Contact()
        {
            return View();
        }
      
        public IActionResult About()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Home()
        {
            CarBookModel obj = new CarBookModel();
            return View(obj);
        }
        [HttpPost]
        public IActionResult Submit(CarBookModel obj)
        {
            _dbContext.carBookModels.Add(obj);
            _dbContext.SaveChanges();
            TempData["msg"] = "Ride Booked Successfully";

            return RedirectToAction("Home");
        }

        public IActionResult Register()
        {
            return View();
        }
       
        public IActionResult RegisterUser(UserRegistrationModel obj)
        {

           _dbContext.userRegistrationModels.Add(obj);
            _dbContext.SaveChanges();
            TempData["msg"] ="Registered Successfully";

            return RedirectToAction("Register");
        }

        [HttpPost]
       
        public IActionResult LoginUser(UserLoginModel obj)
        {
            UserRegistrationModel objLoginModel = _dbContext.userRegistrationModels.Where(m => m.Email == obj.Email && m.Password == obj.Password).SingleOrDefault();

            if(objLoginModel==null)
            {
                TempData["msg"] = "Username or Password is incorrect";

            }
            else
            {
                

                if(objLoginModel.isActive==true)
                {

                    HttpContext.Session.SetString("Name", objLoginModel.Name);
                    HttpContext.Session.SetString("Id", objLoginModel.Id.ToString());
                    HttpContext.Session.SetString("PhoneNumber", objLoginModel.PhoneNumber);


                    if (objLoginModel.Role == "User")
                    {
                        return RedirectToAction("UserPage");

                    }
                    else
                    {
                        return RedirectToAction("AdminPage");


                    }


                }

                else
                {
                    TempData["msg"]= "User is blocked.Please contact admin";


                }
            }

            return RedirectToAction("Login");
        }


        public IActionResult UserPage()
        {
            TempData["Name"] = HttpContext.Session.GetString("Name");

            return View();

        }

        public IActionResult AdminPage()
        {

            return View();
        }

        public async Task<IActionResult> UserList()
        {
            List<CarBookModel> objList = _dbContext.carBookModels.ToList();

            var url = "api/Home/GetBooking";
          
            var response = await  ApiCall.ApiCall.Initial(_configuration).GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse=await response.Content.ReadAsStringAsync();
                objList=JsonConvert.DeserializeObject<List<CarBookModel>>(stringResponse);

            }
 
            return View(objList);
        }

        public IActionResult ChangePassword()
        {

            return View();

        }

        [HttpPost]
        public IActionResult PasswordChanged(string Password)
        {
            int s = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            UserRegistrationModel objStudent = _dbContext.userRegistrationModels.Where(m => m.Id == s).SingleOrDefault();
            objStudent.Password = Password;
            _dbContext.SaveChanges();
            TempData["msg"] = "Password Updated Successfully";
            return RedirectToAction("ChangePassword");

        }
        public IActionResult RegisteredUsersList()
        {

            List<UserRegistrationModel> objList = _dbContext.userRegistrationModels.Where(m=>m.Role=="User").ToList();

            return View(objList);

        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsModel obj)
        {
            _dbContext.contactUsModels.Add(obj);
            _dbContext.SaveChanges();
            TempData["msg"] = "Message Sent Successfully";

            return RedirectToAction("Contact");


        }

        public IActionResult UserResponse()
        {


            List<ContactUsModel> objList = _dbContext.contactUsModels.ToList();

            return View(objList);

        }

        public IActionResult BlockUser(int Id)
        {
            UserRegistrationModel objStudent = _dbContext.userRegistrationModels.Where(m => m.Id == Id).SingleOrDefault();

             objStudent.isActive = false;
            _dbContext.userRegistrationModels.Update(objStudent);
            _dbContext.SaveChanges();
            return RedirectToAction("Home");

        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

           return RedirectToAction("Home");
        }


        public IActionResult UserRides( )
        {

            string PhoneNumber = HttpContext.Session.GetString("PhoneNumber");

            List<CarBookModel> objList =_dbContext.carBookModels.Where(m=>m.PhoneNumber== PhoneNumber).ToList();
                                
            return View(objList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}