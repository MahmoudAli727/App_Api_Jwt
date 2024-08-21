
namespace App_Api_Jwt_Training.Srevices
{
    public interface IStudentSevice
    {
        public Task<List<Student>> GetAllStudents();
        public Task<Student> GetStudentById(int id);
        public Task<Student> AddNewStudent(Student department);
        public Task<bool> UpdateStudent(int id, Student department);
        public Task<bool> DeleteStudent(int id);
    }
}
