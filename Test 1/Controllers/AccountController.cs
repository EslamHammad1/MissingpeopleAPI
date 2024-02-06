namespace Test_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;

        }
        [HttpPost("register")]
        public async Task<IActionResult> register( [FromForm]UserRegisterDTO userDto)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.UserName;

                if (ModelState.IsValid)
                {
                    user.UserName = userDto.Name;
                    user.Email = userDto.Email;
                    IdentityResult result = await userManager.CreateAsync(user, userDto.Password);
                    if (result.Succeeded)
                    {
                        return Ok("account Add Success");
                    }
                    else
                    {
                        return BadRequest(result.Errors.FirstOrDefault());
                    }
                }
                return BadRequest(ModelState);

            }
            return BadRequest(ModelState);

        }

        [HttpPost("Login")]
                public async Task<IActionResult> Login([FromForm]UserLoginDTO userDto)
                {
                    if (ModelState.IsValid == true)
                    {
                        ApplicationUser? user = await userManager.FindByNameAsync(userDto.UserName);
                        if (user != null)
                        {
                            bool fond = await userManager.CheckPasswordAsync(user, userDto.Password);
                            if (fond)
                            {
                                //create clims
                                var claims = new List<Claim>();
                                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                                //create roles 
                                var roles = await userManager.GetRolesAsync(user);
                                foreach (var roleitem in roles)
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, roleitem));
                                }
                                // create signing&& key
                                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                                //create token
                                JwtSecurityToken myToken = new JwtSecurityToken(
                                    issuer: config["JWT:ValidIssuer"],
                                    audience: config["JWT:ValidAudiance"],
                                    claims: claims,
                                    expires: DateTime.Now.AddDays(15),
                                    signingCredentials: signingCredentials
                                    );
                                return Ok(new
                                {
                                    token = new JwtSecurityTokenHandler().WriteToken(myToken),
                                    expiration = myToken.ValidTo
                                });
                            }
                            return Unauthorized();
                        }
                        return Unauthorized();

                    }
                    return Unauthorized();
                }
    }
}