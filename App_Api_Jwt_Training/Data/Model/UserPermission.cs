namespace App_Api_Jwt_Training.Data.Model
{
	public class UserPermission
	{
		public int UserId { get; set; }
		public Permission permissionId { get; set; }
	}
	public enum Permission
	{
		Read=1,
		Add,
		Edit,
		Delete,
	};
}
