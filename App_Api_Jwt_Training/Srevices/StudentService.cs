
namespace App_Api_Jwt_Training.Srevices
{
    public class StudentService : IStudentSevice
    {
        private readonly AppDbContext _appDbContext;

        public StudentService(AppDbContext appContext)
        {
            this._appDbContext = appContext;
        }

        public async Task<Student> AddNewStudent(Student std)
        {
            var stdDp = await _appDbContext.departments.FirstOrDefaultAsync(x => x.Id == std.DeptId);
            if (stdDp == null)
            {
                return null;
            }
            var student = new Student {
                    DeptId = std.DeptId,
                    Email = std.Email,
                    Age = std.Age,
                    Name = std.Name,
                    Image = std.Image,
                    Password = std.Password,
                };
            
            await _appDbContext.students.AddAsync(student);
            await _appDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student= await _appDbContext.students.FirstOrDefaultAsync(x=>x.Id==id);
            if (student==null)
            {
                return false;
            }
            _appDbContext.students.Remove(student);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var students=await _appDbContext.students.ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _appDbContext.students.FirstOrDefaultAsync(x => x.Id == id);
            if (student==null)
            {
                return null;
            }
            return student;
        }

        public async Task<bool> UpdateStudent(int id, Student std)
        {
            var student = await _appDbContext.students.FirstOrDefaultAsync(x => x.Id == id);
            var stdDp = await _appDbContext.departments.FirstOrDefaultAsync(x => x.Id== std.DeptId);

            if (student == null)
            {
                return false;
            }
            if (stdDp == null)
            {
                return false;
            }
            student.DeptId = std.DeptId;
            student.Email = std.Email;
            student.Age = std.Age;
            student.Name = std.Name;
            student.Image = std.Image;
            student.Password = std.Password;
            student.Image = std.Image;
            _appDbContext.students.Update(student);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
