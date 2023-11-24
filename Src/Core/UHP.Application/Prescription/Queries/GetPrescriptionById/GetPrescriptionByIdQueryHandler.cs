using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Exceptions;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.Prescription.Queries.GetPrescriptionById
{
    public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionViewModel>
    {
        private readonly UhpContext _uhpContext;

        public GetPrescriptionByIdQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<PrescriptionViewModel> Handle(GetPrescriptionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var prescription = await _uhpContext.Prescriptions
                .Include(prescription1 => prescription1.Doctor)
                .Include(prescription1 => prescription1.Patient)
                .Include(prescription1 => prescription1.PrescriptionByDrugProducts)
                .ThenInclude(product => product.DrugProduct).ThenInclude(product => product.Drug)
                .SingleOrDefaultAsync(prescription1 => prescription1.Id == request.Id, cancellationToken);

            if (prescription == null)
            {
                throw new NotFoundException("Prescription", request.Id);
            }
            
            return new PrescriptionViewModel()
            {
                Id = prescription.Id,
                QrCode = prescription.QrCodePath,
                DoctorFullName = "Dr. " + prescription.Doctor.Firstname + " " + prescription.Doctor.Lastname,
                PatientId = prescription.PatientId,
                PatientFullName = prescription.Patient.Firstname + " " + prescription.Patient.Lastname,
                TimeStamp = prescription.CreatedAt,
                RedeemAt = prescription.RedeemedAt,
                DrugProductViewModels = prescription.PrescriptionByDrugProducts.Select(product =>
                    new DrugProductViewModel
                    {
                        Id = product.DrugProduct.Id,
                        Cost = product.DrugProduct.Cost,
                        Currency = product.DrugProduct.Currency,
                        Description = product.DrugProduct.Description,
                        Unit = product.DrugProduct.Unit,
                        DrugId = product.DrugProduct.DrugId,
                        DrugName = product.DrugProduct.Drug.Name
                    }).ToList()
            };
        }
    }
}