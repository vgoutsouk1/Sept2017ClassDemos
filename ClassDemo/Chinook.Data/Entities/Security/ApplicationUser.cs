﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chinook.Data.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeID { get; set; }

        public int? CustomerID { get; set; }


    }
}
