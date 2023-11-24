using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UHP.Application.Models;
using UHP.Persistence;

namespace UHP.Application.Drug.Queries.GetDrugs
{
    public class GetDrugsQueryHandler : IRequestHandler<GetDrugsQuery, DrugsListViewModel>
    {
        private readonly UhpContext _uhpContext;

        public GetDrugsQueryHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<DrugsListViewModel> Handle(GetDrugsQuery request, CancellationToken cancellationToken)
        {
            var drugs = _uhpContext.Drugs.Include(drug => drug.DrugProducts).AsQueryable();

            if (request.Sort.ContainsKey("Id"))
            {
                drugs = request.Sort["Id"] == "asc"
                    ? drugs.OrderBy(drug => drug.Id)
                    : drugs.OrderByDescending(drug => drug.Id);
            }
            else if (request.Sort.ContainsKey("Description"))
            {
                drugs = request.Sort["Description"] == "asc"
                    ? drugs.OrderBy(drug => drug.Description)
                    : drugs.OrderByDescending(drug => drug.Description);
            }
            else if (request.Sort.ContainsKey("Name"))
            {
                drugs = request.Sort["Name"] == "asc"
                    ? drugs.OrderBy(drug => drug.Name)
                    : drugs.OrderByDescending(drug => drug.Name);
            }
            else if (request.Sort.ContainsKey("State"))
            {
                drugs = request.Sort["State"] == "asc"
                    ? drugs.OrderBy(drug => drug.State)
                    : drugs.OrderByDescending(drug => drug.State);
            }
            else if (request.Sort.ContainsKey("Toxicity"))
            {
                drugs = request.Sort["Toxicity"] == "asc"
                    ? drugs.OrderBy(drug => drug.Toxicity)
                    : drugs.OrderByDescending(drug => drug.Toxicity);
            }
            else if (request.Sort.ContainsKey("Unii"))
            {
                drugs = request.Sort["Unii"] == "asc"
                    ? drugs.OrderBy(drug => drug.Unii)
                    : drugs.OrderByDescending(drug => drug.Unii);
            }
            else if (request.Sort.ContainsKey("AverageMass"))
            {
                drugs = request.Sort["AverageMass"] == "asc"
                    ? drugs.OrderBy(drug => drug.AverageMass)
                    : drugs.OrderByDescending(drug => drug.AverageMass);
            }
            else if (request.Sort.ContainsKey("CasNumber"))
            {
                drugs = request.Sort["CasNumber"] == "asc"
                    ? drugs.OrderBy(drug => drug.CasNumber)
                    : drugs.OrderByDescending(drug => drug.CasNumber);
            }
            else if (request.Sort.ContainsKey("MonoisotopicMass"))
            {
                drugs = request.Sort["MonoisotopicMass"] == "asc"
                    ? drugs.OrderBy(drug => drug.MonoisotopicMass)
                    : drugs.OrderByDescending(drug => drug.MonoisotopicMass);
            }


            if (request.Filters.ContainsKey("Id") && !string.IsNullOrEmpty(request.Filters["Id"]))
            {
                drugs = drugs.Where(drug => drug.Id.ToString().Equals(request.Filters["Id"]));
            }
            if (request.Filters.ContainsKey("Description") &&
                     !string.IsNullOrEmpty(request.Filters["Description"]))
            {
                drugs = drugs.Where(drug => drug.Description.Contains(request.Filters["Description"]));
            }
            if (request.Filters.ContainsKey("Name") && !string.IsNullOrEmpty(request.Filters["Name"]))
            {
                drugs = drugs.Where(drug => drug.Name.Contains(request.Filters["Name"]));
            }
            if (request.Filters.ContainsKey("State") && !string.IsNullOrEmpty(request.Filters["State"]))
            {
                drugs = drugs.Where(drug => drug.State.Contains(request.Filters["State"]));
            }
            if (request.Filters.ContainsKey("Toxicity") && !string.IsNullOrEmpty(request.Filters["Toxicity"]))
            {
                drugs = drugs.Where(drug => drug.Toxicity.Contains(request.Filters["Toxicity"]));
            }
            if (request.Filters.ContainsKey("Unii") && !string.IsNullOrEmpty(request.Filters["Unii"]))
            {
                drugs = drugs.Where(drug => drug.Unii.Contains(request.Filters["Unii"]));
            }
            if (request.Filters.ContainsKey("AverageMass") &&
                     !string.IsNullOrEmpty(request.Filters["AverageMass"]))
            {
                drugs = drugs.Where(drug => drug.AverageMass.ToString().Contains(request.Filters["AverageMass"]));
            }
            if (request.Filters.ContainsKey("CasNumber") && !string.IsNullOrEmpty(request.Filters["CasNumber"]))
            {
                drugs = drugs.Where(drug => drug.CasNumber.Contains(request.Filters["CasNumber"]));
            }
            if (request.Filters.ContainsKey("MonoisotopicMass") &&
                     !string.IsNullOrEmpty(request.Filters["MonoisotopicMass"]))
            {
                drugs = drugs.Where(drug =>
                    drug.MonoisotopicMass.ToString().Contains(request.Filters["MonoisotopicMass"]));
            }

            return new DrugsListViewModel()
            {
                CurrentPage = request.CurrentPage,
                Length = drugs.Count(),
                Results = await drugs.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).Select(
                    drug =>
                        new DrugViewModel()
                        {
                            Id = drug.Id,
                            Description = drug.Description,
                            Name = drug.Name,
                            State = drug.State,
                            Toxicity = drug.Toxicity,
                            Type = drug.Type,
                            Unii = drug.Unii,
                            AverageMass = drug.AverageMass,
                            CasNumber = drug.CasNumber,
                            MonoisotopicMass = drug.MonoisotopicMass,
                            DrugBankId = drug.DrugBankId,
                            DrugProductViewModels = drug.DrugProducts.Select(product => new DrugProductViewModel()
                            {
                                Id = product.Id,
                                Cost = product.Cost,
                                Currency = product.Currency,
                                Description = product.Description,
                                Unit = product.Unit,
                            }).ToList()
                        }).ToListAsync(cancellationToken),
                PageSize = request.PageSize
            };
        }
    }
}