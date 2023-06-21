﻿using Microsoft.EntityFrameworkCore;     
namespace CabManagementAPI.Models
{
    public class UserRegistrationModel
    {

       
            public int Id { get; set; }
            public string Name { get; set; }

            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public string Role { get; set; }

            public bool isActive { get; set; } = true;



        
    }
}
