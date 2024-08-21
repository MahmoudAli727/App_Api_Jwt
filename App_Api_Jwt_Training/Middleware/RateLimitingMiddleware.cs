
namespace App_Api_Jwt_Training.Middleware
{
	public class RateLimitingMiddleware
	{
		private readonly RequestDelegate _next;
		private static DateTime _REQDate=DateTime.Now;
		private static int _counter = 0;
		public RateLimitingMiddleware(RequestDelegate next)
        {
			this._next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			_counter++;
			if (DateTime.Now.Subtract(_REQDate).Seconds > 10)
			{
				_counter = 1;
				_REQDate = DateTime.Now;
				await _next(context);
			}
			else
			{
				if (_counter > 5)
				{
					_REQDate = DateTime.Now;
					await context.Response.WriteAsync
						("limit Rating");
				}
				else
				{
					_REQDate = DateTime.Now;
					await _next(context);
				}
			}
		}
	}
}





