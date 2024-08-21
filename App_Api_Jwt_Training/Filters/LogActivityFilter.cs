
namespace App_Api_Jwt_Training.Filters
{
	public class LogActivityFilter : IActionFilter,IAsyncActionFilter
	{
		private readonly ILogger<LogActivityFilter> _logger;

		public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
			this._logger = logger;
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on Controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation($"action {context.ActionDescriptor.DisplayName}finished execution on Controller {context.Controller} ");
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation($"(Async) Executing action {context.ActionDescriptor.DisplayName} on Controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
			await next();
			_logger.LogInformation($"(Async) action {context.ActionDescriptor.DisplayName}finished execution on Controller {context.Controller} ");
		}
	}
}
