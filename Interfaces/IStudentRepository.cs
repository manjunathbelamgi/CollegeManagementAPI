public interface IStudentRepository
{
    IEnumerable<Student> GetStudents();
    Student GetById(int id);

    Student Create(Student student);
}