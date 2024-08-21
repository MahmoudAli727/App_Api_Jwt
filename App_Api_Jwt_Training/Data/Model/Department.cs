
namespace App_Api_Jwt_Training.Data.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required ,MaxLength(25)]
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Student> students { get; set; }

    }
}
