
namespace App_Api_Jwt_Training.Filters
{
	public class LogSensitiveActionAttribute:ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			Debug.WriteLine("Sensitive action executed !!!!!!!!!!!! ");
		}
	}
}
