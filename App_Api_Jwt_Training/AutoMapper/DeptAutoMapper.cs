using AutoMapper;

namespace App_Api_Jwt_Training.AutoMapper
{
	public class DeptAutoMapper:Profile
	{
        public DeptAutoMapper()
        {
            CreateMap<DepartmentDto, Department>().ReverseMap();
        }
    }
}
