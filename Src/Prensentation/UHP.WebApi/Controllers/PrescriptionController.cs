using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UHP.Application.Prescription.Command.AddPrescription;
using UHP.Application.Prescription.Command.DeletePrescriptionById;
using UHP.Application.Prescription.Command.ReedemPrescription;
using UHP.Application.Prescription.Command.UpdatePrescription;
using UHP.Application.Prescription.Queries.GetPrescriptionByDoctorId;
using UHP.Application.Prescription.Queries.GetPrescriptionById;
using UHP.Application.User.Queires.GetPrescriptionByPatientId;
using UHP.WebApi.Controllers.Abstract;

namespace UHP.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : BaseController
    {
        
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetPrescriptionById([FromRoute] long id)
        {
            return Ok(await Mediator.Send(new GetPrescriptionByIdQuery()
            {
                Id = id
            }));
        }        
        
        [Route("DoctorId")]
        [HttpGet]
        public async Task<ActionResult> GetPrescriptionByDoctorId()
        {
            return Ok(await Mediator.Send(new GetPrescriptionByDoctorIdQuery()
            {
                DoctorId = GetUserId()
            }));
        }        
        
        [Route("PatientId")]
        [HttpGet]
        public async Task<ActionResult> GetPrescriptionByPatientId()
        {
            return Ok(await Mediator.Send(new GetPrescriptionByPatientIdQuery()
            {
                PatientId = GetUserId()
            }));
        }
        
        [Route("AddPrescription()")]
        [HttpPost]
        public async Task<ActionResult> AddPrescription([FromBody] AddPrescriptionCommandModel command)
        {
            return Ok(await Mediator.Send(new AddPrescriptionCommand
            {
                PatientId = command.PatientId,
                DrugProductsId = command.DrugProductsId,
                DoctorId = GetUserId()
            }));
        }
        
        [Route("UpdatePrescription()")]
        [HttpPost]
        public async Task<ActionResult> UpdatePrescription([FromBody] UpdatePrescriptionCommand command)
        {
            return Ok(await Mediator.Send(command));
        } 
        
        [Route("RedeemPrescription()")]
        [HttpPost]
        public async Task<ActionResult> RedeemPrescription([FromBody] RedeemPrescriptionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }     
        
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeletePrescription([FromRoute] long id)
        {
            return Ok(await Mediator.Send(new DeletePrescriptionByIdCommand()
            {
                Id = id
            }));
        }
    }
}