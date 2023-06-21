using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CabManagementAPI.Models;
using System.Reflection.Metadata.Ecma335;

//using CabManagementAPI.Models;

namespace CabManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("GetStudents")]

        public List<UserRegistrationModel> GetData()
        {
            List<UserRegistrationModel> objList = _dbContext.userRegistrationModels.ToList();

            return objList;

        }

        [HttpGet("GetUsers")]


        public List<ContactUsModel> Data()
        {
            List<ContactUsModel> objlist = _dbContext.contactUsModels.ToList();
            
            return objlist;
            
        }




        [HttpGet("GetBooking")]

        public List<CarBookModel> DataGet()
        {
          List<CarBookModel> objlist =_dbContext.carBookModels.ToList();  
            return objlist;
        }


        




    }
}
