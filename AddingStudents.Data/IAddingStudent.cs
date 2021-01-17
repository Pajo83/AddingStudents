using AddingStudents.Core;
using System;

namespace AddingStudents.Data
{
    public interface IAddingStudent
    {
        Student Create(Student student);
    }
}
