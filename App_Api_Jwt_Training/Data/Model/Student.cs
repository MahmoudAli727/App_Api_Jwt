
namespace App_Api_Jwt_Training.Data.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(25)]
        public string Name { get; set; }

        //[Required, MaxLength(25)]
        //public string Email { get; set; }

        //[Required, MaxLength(25), MinLength(5)]
        //public string Password { get; set; }

        [Required]
        public int Age { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey("department")]        
        public int DeptId { get; set; }
        public virtual Department department { get; set; }
    }
}
