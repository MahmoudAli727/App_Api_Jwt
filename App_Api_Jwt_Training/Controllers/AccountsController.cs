
namespace App_Api_Jwt_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public AccountsController(UserManager<AppUser> userManager, IConfiguration configuration, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
			_appDbContext = appDbContext;
		}

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
		private readonly AppDbContext _appDbContext;

		[HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(NewRegister user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(NewLogin login)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.Password))
                    {
                        var claims = new List<Claim>();
                        //var userId = "userId";
						claims.Add(new Claim(ClaimTypes.Name, user.UserName));
						claims.Add(new Claim(ClaimTypes.Email, user.Email));
						claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
						claims.Add(new Claim(ClaimTypes.Role, "Admin"));
						claims.Add(new Claim(ClaimTypes.Role, "SuperUser"));//--> or and
                        claims.Add(new Claim("userType","Employee"));
                        claims.Add(new Claim("DateBirth","1970-01-01"));

					//	claims.Add(new Claim(userId, user.userId.ToString()));
						claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        //signingCredentials
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience:_configuration["JWT:Audience"],
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: sc
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "User Name is invalid");
                }
            }
            return BadRequest(ModelState);
        }

    }
}
