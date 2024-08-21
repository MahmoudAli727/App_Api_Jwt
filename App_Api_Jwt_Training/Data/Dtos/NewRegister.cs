
namespace App_Api_Jwt_Training.Data.Dtos
{
    public class NewRegister
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage ="this is wrong password")]
        public string PasswoedConfirmed { get; set; }
        [Required]
        public string PhoneNumber{ get; set; }
    }
}
