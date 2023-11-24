using System.Collections.Generic;
using MediatR;
using UHP.Application.Models;

namespace UHP.Application.DrugByDrugInteraction.Queries.CheckPrescriptionValidity
{
    public class CheckPrescriptionValidityQuery : IRequest<CheckPrescriptionValidityViewModel>
    {
        public List<long> DrugId { get; set; } = new();
    }
}