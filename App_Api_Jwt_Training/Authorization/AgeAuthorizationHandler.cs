
namespace App_Api_Jwt_Training.Authorization
{
	public class AgeAuthorizationHandler : AuthorizationHandler<AgeGreaterThan25Requirment>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeGreaterThan25Requirment requirement)
		{
			var dob = DateTime.Parse(context.User.FindFirstValue("DateBirth"));

			if (DateTime.Today.Year - dob.Year >= 25)
			{
				context.Succeed(requirement);
			}
			return Task.CompletedTask;
		}
	}
}
