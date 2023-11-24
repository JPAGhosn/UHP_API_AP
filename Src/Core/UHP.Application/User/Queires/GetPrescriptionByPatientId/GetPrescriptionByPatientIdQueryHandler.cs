using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.User.Queires.GetPrescriptionByPatientId
{
    public class
        GetPrescriptionByPatientIdQueryHandler : IRequestHandler<GetPrescriptionByPatientIdQuery,
            PrescriptionListViewModel>
    {
        private readonly UhpContext _uhpContext;

        public GetPrescriptionByPatientIdQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<PrescriptionListViewModel> Handle(GetPrescriptionByPatientIdQuery request,
            CancellationToken cancellationToken)
        {
            var prescription = _uhpContext.Prescriptions.Include(prescription => prescription.Patient)
                .Include(prescription => prescription.Doctor)
                .Include(prescription1 => prescription1.PrescriptionByDrugProducts)
                .ThenInclude(product => product.DrugProduct).ThenInclude(product => product.Drug)
                .Where(prescription => prescription.PatientId == request.PatientId);

            var unredeemedPres =
                await prescription.Where(model => !model.RedeemedAt.HasValue).ToListAsync(cancellationToken);
            var redeemedPres =
                await prescription.Where(model => model.RedeemedAt.HasValue).ToListAsync(cancellationToken);

            return new PrescriptionListViewModel()
            {
                RedeemedPrescriptionViewModels = redeemedPres.Select(prescription =>
                    new PrescriptionViewModel()
                    {
                        Id = prescription.Id,
                        DoctorFullName = "Dr. " + prescription.Doctor.Firstname + " " + prescription.Doctor.Lastname,
                        PatientFullName = prescription.Patient.Firstname + " " + prescription.Patient.Lastname,
                        PatientId = prescription.PatientId,
                        QrCode = prescription.QrCodePath,
                        TimeStamp = prescription.CreatedAt,
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
                    }).ToList(),
                UnredeemedPrescriptionViewModels = unredeemedPres.Select(prescription =>
                    new PrescriptionViewModel()
                    {
                        Id = prescription.Id,
                        DoctorFullName = "Dr. " + prescription.Doctor.Firstname + " " + prescription.Doctor.Lastname,
                        PatientFullName = prescription.Patient.Firstname + " " + prescription.Patient.Lastname,
                        PatientId = prescription.PatientId,
                        QrCode = prescription.QrCodePath,
                        TimeStamp = prescription.CreatedAt,
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
                    }).ToList()
            };
        }
    }
}