using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Application.Prescription.Queries.GetPrescriptionById;
using UHP.Domain.Models.Public;
using UHP.Infrastructure.Interfaces;
using UHP.Persistence;

namespace UHP.Application.Prescription.Command.AddPrescription
{
    public class
        AddPrescriptionCommandHandler : IRequestHandler<AddPrescriptionCommand, PrescriptionViewModel>
    {
        private readonly UhpContext _uhpContext;

        private readonly IMediator _mediator;

        private readonly IQrCodeService _qrCodeService;
        private readonly IMailService _mailService;
        private readonly IFileSystemService _fileSystemService;

        public AddPrescriptionCommandHandler(UhpContext uhpContext, IMediator mediator, IQrCodeService qrCodeService,
            IMailService mailService, IFileSystemService fileSystemService)
        {
            _uhpContext = uhpContext;
            _mediator = mediator;
            _qrCodeService = qrCodeService;
            _mailService = mailService;
            _fileSystemService = fileSystemService;
        }

        public async Task<PrescriptionViewModel> Handle(AddPrescriptionCommand request,
            CancellationToken cancellationToken)
        {
            var prescription = new Domain.Models.Public.Prescription
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                PrescriptionByDrugProducts = request.DrugProductsId.Select(l => new PrescriptionByDrugProduct()
                {
                    DrugProductId = l
                }).ToList(),
            };

            await _uhpContext.AddAsync(prescription, cancellationToken);
            await _uhpContext.SaveChangesAsync(cancellationToken);

            prescription.QrCodePath = await _qrCodeService.GenerateQrCode(prescription.Id, cancellationToken);

            _uhpContext.Prescriptions.Update(prescription);
            await _uhpContext.SaveChangesAsync(cancellationToken);

            var user = await _uhpContext.Users.SingleOrDefaultAsync(user1 => user1.Id == request.PatientId,
                cancellationToken);
            var dr = await _uhpContext.Users.SingleOrDefaultAsync(user1 => user1.Id == request.DoctorId,
                cancellationToken);

            _mailService.Send("New Prescription",
                Helpers.HtmlEmailHelper.HtmlPrescriptionEmail("Dr. " + dr.Firstname + " " + dr.Lastname,
                    user.Firstname + " " + user.Lastname),
                user.Email, Path.Combine(_fileSystemService.GetRootPath(), prescription.QrCodePath));

            return await _mediator.Send(new GetPrescriptionByIdQuery()
            {
                Id = prescription.Id
            }, cancellationToken);
        }
    }
}