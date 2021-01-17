using AddingStudents.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddingStudents.Data
{
    public class AddingStudentDbContext : DbContext
    {
        public AddingStudentDbContext(DbContextOptions<AddingStudentDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<StudentSubject> StudentSubjects { get; set; }
    }
}
