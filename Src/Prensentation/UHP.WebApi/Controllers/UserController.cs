using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UHP.Application.User.Authentication.Login;
using UHP.Application.User.Authentication.SignUp;
using UHP.Application.User.Queires.GetPatients;
using UHP.WebApi.Controllers.Abstract;

namespace UHP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [Route("Authenticate/Login()")]
        [HttpPost]
        public async Task<ActionResult> AuthenticateLogin([FromBody] LoginUserRequestModel query)
        {
            return Ok(await Mediator.Send(query));
        }      
        
        [AllowAnonymous]
        [Route("Authenticate/SignUp()")]
        [HttpPost]
        public async Task<ActionResult> AuthenticateSignUp([FromBody] SignUpUserRequestModel query)
        {
            return Ok(await Mediator.Send(query));
        }
        
        [Route("Authenticate/ValidateToken()")]
        [HttpGet]
        public async Task<ActionResult> AuthenticateValidateToken()
        {
            return Ok();
        }
        
        [Route("GetPatients")]
        [HttpGet]
        public async Task<ActionResult> GetPatients([FromQuery] string value)
        {
            return Ok(await Mediator.Send(new GetPatientsQuery
            {
                Patient = value
            }));
        }
    }
}