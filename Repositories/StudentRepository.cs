using CollegeManagementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

public class StudentRepository: IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();

    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(n => n.id == id);

    }

    public async Task<Student> CreateAsync(Student student)
    {
        // var newId = _context.Students
        //                     .OrderBy(s => s.id)
        //                     .LastOrDefault();

       // student.id = newId;
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }
    public async Task<Student> UpdateAsync(Student student)
    {
        var res = await _context.Students.FirstOrDefaultAsync(n => n.id == student.id);
        if (res == null)
        {
            return null;
        }
        res.id = student.id;
        res.name = student.name;
        res.email = student.email;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var vari = await _context.Students.FirstOrDefaultAsync(n => n.id == id);
        if (vari == null)
        {
            return false;
        }
        var res = _context.Students.Remove(vari);
        await _context.SaveChangesAsync();
        return true;
    }
    
}