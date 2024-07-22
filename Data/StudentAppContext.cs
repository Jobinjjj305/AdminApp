#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Data
{
    public class StudentAppContext : DbContext
    {
        public DbSet<StudentApp.Models.User> Users { get; set; }
        public DbSet<StudentApp.Models.UserRegistration> tbl_UserRegistration { get; set; }

        public StudentAppContext(DbContextOptions<StudentAppContext> options)
       : base(options)
        {
        }
        public DbSet<StudentApp.Models.Student> Student { get; set; }
        
    }
}
