
namespace App_Api_Jwt_Training.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IOptions<Jwt> _jwt;

		public ConfigController(IConfiguration configuration, IOptions<Jwt> jwt)
        {
			this._configuration = configuration;
			this._jwt = jwt;
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var config = new
			{
				AllowedHosts = _configuration.GetConnectionString("DefaultConnection"),
				Jwt=_jwt.Value,
			};
			return Ok(config);
		}
    }
}
