
namespace App_Api_Jwt_Training.Srevices
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAllDepts();
        public Task<Department> GetDepartmentById(int id);
        public Task<Department> AddNewDepartment(Department department);
        public Task<bool> UpdateDepartment(int id,Department department);
        public Task<bool> DeleteDepartment(int id);
    }
}
