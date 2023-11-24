using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UHP.Application.Drug.Command.AddOrUpdateDrug;
using UHP.Application.Drug.FileUpload.ReadXmlDrugFile;
using UHP.Application.Drug.Queries.GetDrugById;
using UHP.Application.Drug.Queries.GetDrugs;
using UHP.Application.Drug.Queries.GetDrugsSearch;
using UHP.Application.DrugProducts.Queries.GetDrugProductsByDrugId;
using UHP.WebApi.Controllers.Abstract;

namespace UHP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : BaseController
    {
        
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetDrugById([FromRoute] long id)
        {
            return Ok(await Mediator.Send(new GetDrugByIdQuery()
            {
                Id = id
            }));
        }  
        
        [Route("GetDrugs")]
        [HttpPost]
        public async Task<ActionResult> GetDrugs([FromBody] GetDrugsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }   
        
        
        [Route("GetDrugsSearch")]
        [HttpGet]
        public async Task<ActionResult> GetDrugsSearch([FromQuery] string value)
        {
            return Ok(await Mediator.Send(new GetDrugsSearchQuery()
            {
                Value = value
            }));
        }   
        
        [Route("{drugId}/DrugProducts")]
        [HttpGet]
        public async Task<ActionResult> GetDrugProducts([FromRoute] long drugId)
        {
            return Ok(await Mediator.Send(new GetDrugProductsByDrugIdQuery()
            {
                DrugId = drugId
            }));
        }
                
        
        [Route("AddOrUpdateDrug()")]
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateDrug([FromBody] AddOrUpdateDrugCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [Route("XmlUpload()")]
        [HttpPost]
        [DisableRequestSizeLimit] 
        public async Task<ActionResult> DrugXmlUpload([FromForm] ReadXmlDrugFileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}