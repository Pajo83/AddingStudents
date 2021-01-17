using AddingStudents.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddingStudents.Data
{
    public class AddingStudentsSqlData: IAddingStudent
    {
        private readonly AddingStudentDbContext addingStudentDb; 

        public AddingStudentsSqlData(AddingStudentDbContext addingStudentDb)
        {
            this.addingStudentDb = addingStudentDb;
        }

        public Student Create(Student student)
        {
            addingStudentDb.Add(student);
            return student;
        }
    }
}
