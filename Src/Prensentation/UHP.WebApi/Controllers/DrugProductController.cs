using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UHP.Application.Drug.Queries.GetDrugById;
using UHP.Application.DrugProducts.Queries.GetDrugProductById;
using UHP.WebApi.Controllers.Abstract;

namespace UHP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrugProductController : BaseController
    {
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetDrugById([FromRoute] long id)
        {
            return Ok(await Mediator.Send(new GetDrugProductByIdQuery()
            {
                Id = id
            }));
        }  

    }
}