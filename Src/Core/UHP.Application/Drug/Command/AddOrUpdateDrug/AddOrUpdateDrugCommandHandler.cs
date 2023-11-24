using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Drug.Queries.GetDrugById;
using UHP.Application.Models;
using UHP.Domain.Models.Drugs;
using UHP.Persistence;

namespace UHP.Application.Drug.Command.AddOrUpdateDrug
{
    public class AddOrUpdateDrugCommandHandler : IRequestHandler<AddOrUpdateDrugCommand, DrugViewModel>
    {
        private readonly UhpContext _uhpContext;
        private readonly IMediator _mediator;

        public AddOrUpdateDrugCommandHandler(UhpContext uhpContext, IMediator mediator)
        {
            _uhpContext = uhpContext;
            _mediator = mediator;
        }

        public async Task<DrugViewModel> Handle(AddOrUpdateDrugCommand request, CancellationToken cancellationToken)
        {
            var drug = request.Id != 0
                ? await _uhpContext.Drugs.Include(drug1 => drug1.DrugProducts)
                    .SingleOrDefaultAsync(drug1 => drug1.Id == request.Id, cancellationToken)
                : new Domain.Models.Drugs.Drug();


            drug.Type = request.Type;
            drug.Name = request.Name;
            drug.Description = request.Description;
            drug.CasNumber = request.CasNumber;
            drug.Unii = request.Unii;
            drug.AverageMass = request.AverageMass;
            drug.MonoisotopicMass = request.MonoisotopicMass;
            drug.State = request.State;
            drug.Toxicity = request.Toxicity;
            
            foreach (var command in request.AddDrugProductsCommands)
            {
                var drugProduct = command.Id != 0 ?
                    drug.DrugProducts.Single(productsCommand =>
                        productsCommand.Id == command.Id) : new DrugProduct()
                    {
                        DrugId = drug.Id
                    };
                
                drugProduct.Cost = command.Cost;
                drugProduct.Currency = command.Currency;
                drugProduct.Description = command.Description;
                drugProduct.Unit = command.Unit;
                
                _uhpContext.DrugProducts.Update(drugProduct);
            }
            
            var drugsProductsIds = request.AddDrugProductsCommands.Select(command => command.Id);
            var deleteDrugsProducts = drug.DrugProducts.Where(product => !drugsProductsIds.Contains(product.Id)).ToList();
            
            _uhpContext.DrugProducts.RemoveRange(deleteDrugsProducts);
            drug = _uhpContext.Drugs.Update(drug).Entity;
            await _uhpContext.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetDrugByIdQuery()
            {
                Id = drug.Id
            }, cancellationToken);
        }
    }
}