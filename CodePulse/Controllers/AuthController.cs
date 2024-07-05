using CodePulse.Models.DTOs;
using CodePulse.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUser.Email?.Trim(),
                Email = registerUser.Email?.Trim(),
            };

            //Create User
            var identityResult = await _userManager.CreateAsync(identityUser,registerUser.Password);

            if(identityResult.Succeeded)
            {
                //Add role to user
                identityResult = await _userManager.AddToRoleAsync(identityUser,"Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser) 
        {
            var userFromDb = await _userManager.FindByEmailAsync(loginUser.Email);
            if (userFromDb != null)
            {
                //check password
                var isValidPassword = await _userManager.CheckPasswordAsync(userFromDb,loginUser.Password);
                if (isValidPassword)
                {
                    var roles = await _userManager.GetRolesAsync(userFromDb);

                    //generate token
                    var token = _tokenRepository.CreateJwtToken(userFromDb, roles: roles.ToList());
                    var response = new LoginResponseDto
                    {
                        Email = loginUser.Email,
                        Roles = roles.ToList(),
                        Token = token
                    };

                    return Ok(response);
                }
            }
            ModelState.AddModelError("","Invalid credentials !");
            return ValidationProblem(ModelState);
        }
    }
}
