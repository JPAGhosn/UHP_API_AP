using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace UHP.WebApi.Controllers.Abstract
{
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected long GetUserId()
        {
            var accessToken = Request.Headers["Authorization"];

            var stream = accessToken[0].Substring(7, accessToken[0].Length - 7);

            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;


            var id = tokenS?.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value;
            return long.Parse(id);
        }
    }
}