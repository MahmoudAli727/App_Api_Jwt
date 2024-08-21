
namespace App_Api_Jwt_Training.Srevices
{
    public class DepartmentService : IDepartmentService
    {

        private readonly AppDbContext _appDbContext;
        public DepartmentService(AppDbContext appContext) 
        {
            this._appDbContext = appContext;
        }

        public async Task<Department> AddNewDepartment(Department department)
        {
            await _appDbContext.departments.AddAsync(department);
            await _appDbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var dept = await _appDbContext.departments.FirstOrDefaultAsync(c=>c.Id==id);
            if (dept==null)
            {
                return false;
            }
            _appDbContext.departments.Remove(dept);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Department>> GetAllDepts()
        {
            var Depts = await _appDbContext.departments.ToListAsync();
            return Depts;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var Dept = await _appDbContext.departments.FirstOrDefaultAsync(x=>x.Id== id);
            if (Dept==null)
            {
                return null;
            }
            return Dept;
        }

        public async Task<bool> UpdateDepartment(int id, Department department)
        {
            var dept=await _appDbContext.departments.FirstOrDefaultAsync(x=>x.Id==id);
            if (dept==null)
            {
                return false;
            }
            dept.Name = department.Name;
            _appDbContext.Update(dept);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
