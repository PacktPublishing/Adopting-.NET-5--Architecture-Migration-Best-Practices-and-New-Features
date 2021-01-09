using System.Collections.Generic;

public interface IStudentRepository
{
    IEnumerable<Student> GetAllStudents();
    Student Get(int Id);
    Student Add(Student student);
    Student Update(Student changedStudent);
    Student Delete(int Id);
}