namespace App_Api_Jwt_Training.Data.Dtos
{
	public class Jwt
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SecretKey { get; set; }
	}
}
