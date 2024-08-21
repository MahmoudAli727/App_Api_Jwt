
namespace App_Api_Jwt_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    
    public class StudentsController : ControllerBase
    {
        private readonly IStudentSevice _studentSevice;

        public AppDbContext _appDbContext { get; }

        public StudentsController(IStudentSevice studentSevice,AppDbContext appDbContext )
        {
            this._studentSevice = studentSevice;
            _appDbContext = appDbContext;
        }

        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var students = await _studentSevice.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GwtStudentById")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentSevice.GetStudentById(id);
                if (student == null)
                {
                    return NotFound($"Yhis id {id} is not exist");
                }
                return Ok(student);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var stream = new MemoryStream();
                    await studentDto.Image.CopyToAsync(stream);
                    var newStudent = new Student
                    {
                        Name = studentDto.Name,
                        Age = studentDto.Age,
                        DeptId = studentDto.DeptId,
                        Email = studentDto.Email,
                        Password = studentDto.Password,
                        Image = stream.ToArray(),
                    };
                    var std= await _studentSevice.AddNewStudent(newStudent);
                    if (std==null)
                    {
                        return BadRequest("wrong massage,tray again ");
                    }
                    return Ok(std);
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut("Updatetudent")]
        public async Task<IActionResult> UpdateStudent(int id,StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Student student = new Student();
                    student.Name = studentDto.Name;
                    student.Age = studentDto.Age;
                    student.DeptId = studentDto.DeptId;
                    student.Email = studentDto.Email;
                    student.Password = studentDto.Password;
                    if (studentDto.Image != null)
                    {
                        using var stream = new MemoryStream();
                        await studentDto.Image.CopyToAsync(stream);
                        student.Image = stream.ToArray();
                    }
                    else
                    {
                        student.Image = _appDbContext.students.FirstOrDefault(x => x.Id == id).Image;
                    }
                    var std = await _studentSevice.UpdateStudent(id,student);
                    if (std == false)
                        return NotFound($"Yhis id {id} is not exist Or Deptid {studentDto.DeptId} is not exist");
                    return Ok("Updated Successfully");
                }catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("someThing is wrong");
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var std = await _studentSevice.DeleteStudent(id);
                if (std == false)
                {
                    return NotFound($"Yhis id {id} is not exist");
                }
                return Ok("Deleted Successfully");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }
    }
}

//var stdDtos = new List<StudentDto>();
//foreach (var student in students) {
//         stdDtos.Add(new StudentDto { 
//             Name = student.Name,
//             Email = student.Email,
//             Age = student.Age,
//             Password = student.Password,
//             DeptId = student.Id,
//         });
//}
