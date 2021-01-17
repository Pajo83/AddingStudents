using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AddingStudents.Core;
using AddingStudents.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AddingStudents
{
    public class AddStudentModel : PageModel
    {
        private readonly AddingStudentDbContext _context;

        public AddStudentModel(AddingStudentDbContext context)
        {
            _context = context;
        }

        public bool hasError;
        public string errorMessage;
        public string successMessage;

        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public List<Subject> Subjects { get; set; }

        public SelectList SubjectsList;
        public SelectList SelectedSubjectList;

        public IActionResult OnGet()
        {
            hasError = false;

            Subjects = _context.Subjects.ToList();
            SubjectsList = new SelectList(Subjects, "SubjectId", "Name", 0);


            return Page();
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public IActionResult OnPost([Bind] Student studentSubmit)
        {
            hasError = false;
            Subjects = _context.Subjects.ToList();
            SubjectsList = new SelectList(Subjects, "SubjectId", "Name", 0);

            var selectedSubjects = this.Request.Form["SelectedSubjectList"];
            var selectedCount = selectedSubjects.Count();
            if (selectedCount < 2)
            {
                hasError = true;
                errorMessage = "You must select at least 2 subjects.";
                return Page();
            }

            if (!IsValidEmail(studentSubmit.Email))
            {
                hasError = true;
                errorMessage = "Invalid Email.";
                return Page();
            }

            if (studentSubmit.MobilePhone.Length != 9 || (studentSubmit.MobilePhone[0] != '0' || studentSubmit.MobilePhone[1] != '7'))
            {
                hasError = true;
                errorMessage = "Invalid phone number.";
                return Page();
            }

            var studentFound = _context.Students.Where(s => s.StudentNo == studentSubmit.StudentNo).Count();
            if (studentFound != 0)
            {
                hasError = true;
                errorMessage = "Student with that student number already exists.";
                return Page();
            }

            studentFound = _context.Students.Where(s => s.Email == studentSubmit.Email).Count();
            if (studentFound != 0)
            {
                hasError = true;
                errorMessage = "Student with that student email already exists.";
                return Page();
            }



            _context.Students.Add(studentSubmit);
            _context.SaveChanges();
            int StudentId = studentSubmit.StudentId;

            for (int i = 0; i < selectedSubjects.Count(); i++)
            {
                StudentSubject studentSubject = new StudentSubject();
                studentSubject.StudentId = StudentId;
                studentSubject.SubjectId = Convert.ToInt32(selectedSubjects[i]);
                _context.StudentSubjects.Add(studentSubject);
            }

            _context.SaveChanges();

            successMessage = "Student has been saved.";


            return Page();
        }
    }
}