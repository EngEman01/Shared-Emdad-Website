using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StyleHub.Models;

namespace StyleHub.Utility
{
    // Static Details
    public static class SD
    {
        public const string Role_User_Name = "User"; 
        public const string Role_Admin_Name = "Admin"; 

        public const string ADMIN_EMAIL = "admin_main@gmail.com";
        public const string ADMIN_PASSWORD = "Admin*Main1234";

        public const string Admin_Area = "Admin";
        public const string Customer_Area = "Customer";
        public const string Identity_Area = "Identity";



        // Roles
        public static IdentityRole AdminRole = new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = "1"
        };

        public static IdentityRole UserRole = new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER",
            Id = "2"
        };

       


    }
}
