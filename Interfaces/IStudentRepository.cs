public interface IStudentRepository
{
     Task<IEnumerable<Student>> GetStudentsAsync();
     Task<Student> GetByIdAsync(int id);

     Task<Student> CreateAsync(Student student);

     Task<Student> UpdateAsync(Student student);

     Task<bool> DeleteStudentAsync(int id);
}