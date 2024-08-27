using AutoMapper;

namespace App_Api_Jwt_Training.AutoMapper
{
	public class StudentAutoMapper:Profile
	{
        public StudentAutoMapper()
        {
            CreateMap<StudentDto, Student>().
               ForMember(src => src.Image, opt => opt.Ignore());
        }
    }
}
