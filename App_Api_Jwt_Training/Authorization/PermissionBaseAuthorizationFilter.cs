
namespace App_Api_Jwt_Training.Authorization
{
	public class PermissionBaseAuthorizationFilter(AppDbContext appDbContext) : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
			if (attribute != null)
			{
				var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
				if (claimIdentity == null || !claimIdentity.IsAuthenticated)
				{
					context.Result = new ForbidResult();
				}
				else
				{
					var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
					var hasPermission = appDbContext.Set<UserPermission>().Any(x => x.UserId == userId &&
					x.permissionId == attribute.Permission);
					if (!hasPermission)
					{
						context.Result = new ForbidResult();
					}
				}
			}
		}
	}
}
