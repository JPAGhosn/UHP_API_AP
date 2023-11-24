using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UHP.Application.Drug.Queries.GetDrugById;
using UHP.Application.DrugByDrugInteraction.Queries.CheckPrescriptionValidity;
using UHP.WebApi.Controllers.Abstract;

namespace UHP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrugByDrugInteractionController : BaseController
    {
        [Route("CheckInteraction")]
        [HttpGet]
        public async Task<ActionResult> CheckInteraction([FromQuery] List<long> drugId)
        {
            return Ok(await Mediator.Send(new CheckPrescriptionValidityQuery()
            {
                DrugId = drugId
            }));
        }  
    }
}