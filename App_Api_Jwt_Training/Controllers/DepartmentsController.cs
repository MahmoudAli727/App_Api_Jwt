using AutoMapper;

namespace App_Api_Jwt_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [LogSensitiveAction]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
		private readonly IMapper _mapper;

		public DepartmentsController(IDepartmentService IdepartmentService, IMapper mapper)
        {
            _departmentService = IdepartmentService;
			_mapper = mapper;
		}

        //[CheckPermission(Permission.Read)]
        //[Authorize(Roles ="Admin,SuperUser")] //--> Admin or SuperUser
        //[Authorize(Roles ="SuperUser")] //-->  SuperUser
        //                                //     and
        //[Authorize(Roles ="Admin")]     //-->  Admin 
        //[Authorize(Policy = "AgeGreaterThan25")]
        [Authorize]
        [HttpGet("getAllDepartments")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
               return Ok(await _departmentService.GetAllDepts());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
			}
        }        

		//[CheckPermission(Permission.Read)]
        [HttpGet("GwtDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var Dept = await _departmentService.GetDepartmentById(id);
                if (Dept == null)
                {
                    return NotFound($"this id {id} is not exist");
                }
                var DeptDto = _mapper.Map<DepartmentDto>(Dept);
                    //new DepartmentDto { Name = Dept.Name };
                return Ok(DeptDto);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDept(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newDept = _mapper.Map<Department>(departmentDto);
					//    new Department
					//{
					//    Name = departmentDto.Name,
					//};
					var dept=await _departmentService.AddNewDepartment(newDept);
                    return Ok(dept);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdaateDept(int id, DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var dept = _mapper.Map<Department>(departmentDto);
				 //Department department = new Department();
				 //department.Name = departmentDto.Name;
				try {
                    var UpdateBool = await _departmentService.UpdateDepartment(id, dept);
                    if (!UpdateBool)
                    {
                        return NotFound($"this id {id} is not exist");
                    }
                    return Ok("Updated Successfully");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var DeptBool = await _departmentService.DeleteDepartment(id);
                if (DeptBool == false)
                {
                    return NotFound($"this id {id} is not exist");
                }
                return Ok("Deleted Successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
