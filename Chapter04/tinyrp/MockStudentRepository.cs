using System.Collections.Generic;
using System.Linq;

public class MockStudentRepository : IStudentRepository
{
    private HashSet<Student> students;    
    public MockStudentRepository()
    {
        students = new HashSet<Student>()
        {
            new Student() { Id = 1, Name = "Student1", Email = "one@something.com" },
            new Student() { Id = 2, Name = "Student2", Email = "two@something.com" },
            new Student() { Id = 3, Name = "Student3", Email = "three@something.com" },
        };
    }
    
    public Student Add(Student Student)
    {
        Student.Id = students.Max(e => e.Id) + 1;
        students.Add(Student);
        return Student;
    }
    public Student Delete(int Id)
    {
        Student Student = students.FirstOrDefault(e => e.Id == Id);
        if (Student != null)
        {
            students.Remove(Student);
        }
        return Student;
    }
    public IEnumerable<Student> GetAllStudents()
    {
        return students;
    }
    public Student Get(int Id)
    {
        return this.students.FirstOrDefault(e => e.Id == Id);
    }
    public Student Update(Student StudentChanges)
    {
        Student Student = students.FirstOrDefault(e => e.Id == StudentChanges.Id);
        if (Student != null)
        {
            Student.Name = StudentChanges.Name;
            Student.Email = StudentChanges.Email;
        }
        return Student;
    }
}