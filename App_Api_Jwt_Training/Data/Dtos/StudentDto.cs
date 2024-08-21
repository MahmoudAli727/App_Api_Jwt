
namespace App_Api_Jwt_Training.Data.Dtos
{
    public class StudentDto
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Required, MaxLength(25)]
        public string Email { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string Password { get; set; }

        [Required]
        public int Age { get; set; }
        
        public IFormFile? Image { get; set; }
        public int DeptId { get; set; }
    }
}
