using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;
using UserService.Util;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAdministratorRepository _adminRepo;
        private readonly IPersonalRepository _personalRepo;
        private readonly IUserRepository _userRepo;
        private readonly JwtGenerator _jwt;

        public AuthController(IAdministratorRepository adminRepo, IPersonalRepository personalRepo, IUserRepository userRepo, JwtGenerator jwt)
        {
            _adminRepo = adminRepo;
            _personalRepo = personalRepo;
            _userRepo = userRepo;
            _jwt = jwt;
        }


        /// <summary>
        /// Login admin
        /// </summary>
        /// <returns>token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public ActionResult LoginAdmin([FromBody] Principal principal) 
        { 
            var admin = _adminRepo.GetByEmail(principal.Email);

            if (admin == null)
                return Forbid("User does not exist");

            if (admin.Password != principal.Password)
                return Unauthorized("Password is not correct");


            return Ok(_jwt.GenerateJwt("admin"));

        }

        /// <summary>
        /// Login personal
        /// </summary>
        /// <returns>token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public ActionResult LoginPersonal([FromBody] Principal principal)
        {
            var personal = _personalRepo.GetByEmail(principal.Email);

            if (personal == null)
                return Forbid("User does not exist");

            if (personal.Password != principal.Password)
                return Unauthorized("Password is not correct");


            return Ok(_jwt.GenerateJwt("personal"));
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns>token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public ActionResult LoginUser([FromBody] Principal principal)
        {
            var user = _userRepo.GetByEmail(principal.Email);

            if (user == null)
                return Forbid("User does not exist");

            if (user.Password != principal.Password)
                return Unauthorized("Password is not correct");


            return Ok(_jwt.GenerateJwt("user"));
        }
    }
}
