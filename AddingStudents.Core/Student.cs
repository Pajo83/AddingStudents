using System;
using System.Collections.Generic;

namespace AddingStudents.Core
{
    public class Student
    {
        public Student()
        {
            this.Subjects = new HashSet<StudentSubject>();
        }

        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string StudentNo { get; set; }
        public HashSet<StudentSubject> Subjects { get; set; }
    }
}
