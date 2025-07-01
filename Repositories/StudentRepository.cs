using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class StudentRepository: IStudentRepository
{
    public IEnumerable<Student> GetStudents()
    {
        return StudentRepository.students;
        
    }

    public Student GetById(int id)
    {
        return StudentRepository.students.FirstOrDefault(n => n.id == id);
    }

    public Student Create(Student student)
    {
        var newId = StudentRepository.students.LastOrDefault().id + 1;

        student.id = newId;
        StudentRepository.students.Add(student);
        return student;
    }
    public Student Update(Student student)
    {
        var res = StudentRepository.students.FirstOrDefault(n => n.id == student.id);
        if (res == null)
        {
            return null;
        }
        res.id = student.id;
        res.name = student.name;
        res.email = student.email;
        return res;
    }

    public bool DeleteStudent(int id)
    {
        var vari = StudentRepository.students.FirstOrDefault(n => n.id == id);
        if (vari == null)
        {
            return false;
        }
        return StudentRepository.students.Remove(vari);
    }
    public static List<Student> students { get; set; } = new List<Student>()
    {
        new Student
        {
            id=1,
            name="manjunath",
            email="manju@gmail.com"
        },
        new Student
        {
            id=2,
            name="akash",
            email="akash@gmail.com"
         },
         new Student{
            id=3,
            name="Abhishek",
            email="abhi@gmail.com"
         }

    };
}