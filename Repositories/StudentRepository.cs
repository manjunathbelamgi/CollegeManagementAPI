public class StudentRepository: IStudentRepository
{
    public IEnumerable<Student> GetStudents()
    {
        var all = StudentRepository.students;
        return all;
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