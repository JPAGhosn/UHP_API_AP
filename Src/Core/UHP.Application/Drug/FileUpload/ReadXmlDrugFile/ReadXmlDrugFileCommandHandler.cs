using System;
using System.Collections.Generic;
using MediatR;
using UHP.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Force.DeepCloner;
using UHP.Domain.Models.Drugs;

namespace UHP.Application.Drug.FileUpload.ReadXmlDrugFile
{
    public class ReadXmlDrugFileCommandHandler : IRequestHandler<ReadXmlDrugFileCommand>
    {
        private readonly UhpContext _uhpContext;

        public ReadXmlDrugFileCommandHandler(UhpContext uhpContext)
        {
            _uhpContext = uhpContext;
        }

        public async Task<Unit> Handle(ReadXmlDrugFileCommand request, CancellationToken cancellationToken)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                Async = true
            };

            var drug = new Domain.Models.Drugs.Drug();
            var drugs = new List<Domain.Models.Drugs.Drug>();
            var drugPrice = new DrugProduct();
            var drugPrimaryKey = false;
            int depth = 0;
            int i = 0;
            var element = "";
            var parentElementPrice = false;
            using (var reader = XmlReader.Create(request.XmlFile.OpenReadStream(), settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            element = reader.Name;
                            depth = reader.Depth;
                            if (reader.Name == "drugbank-id" && depth == 2 && reader.HasAttributes)
                            {
                                drugPrimaryKey = Boolean.Parse(reader.GetAttribute("primary"));
                            }

                            if (depth == 3 && reader.Name == "price")
                            {
                                parentElementPrice = true;
                            }

                            if (depth == 4 && reader.Name == "cost" && parentElementPrice)
                            {
                                drugPrice.Currency = reader.GetAttribute("currency");
                            }

                            break;
                        case XmlNodeType.Text:
                            if (depth == 2)
                            {
                                if (element == "name")
                                {
                                    drug.Name = await reader.GetValueAsync();
                                }
                                else if (element == "drugbank-id" && drugPrimaryKey)
                                {
                                    drug.DrugBankId = await reader.GetValueAsync();
                                }
                                else if (element == "description")
                                {
                                    drug.Description = await reader.GetValueAsync();
                                }
                                else if (element == "cas-number")
                                {
                                    drug.CasNumber = await reader.GetValueAsync();
                                }
                                else if (element == "unii")
                                {
                                    drug.Unii = await reader.GetValueAsync();
                                }
                                else if (element == "average-mass")
                                {
                                    drug.AverageMass = double.Parse(await reader.GetValueAsync());
                                }
                                else if (element == "monoisotopic-mass")
                                {
                                    drug.MonoisotopicMass = double.Parse(await reader.GetValueAsync());
                                }
                                else if (element == "state")
                                {
                                    drug.State = await reader.GetValueAsync();
                                }
                                else if (element == "toxicity")
                                {
                                    drug.Toxicity = await reader.GetValueAsync();
                                }
                            }
                            else if (depth == 4 && parentElementPrice)
                            {
                                if (element == "description")
                                {
                                    drugPrice.Description = await reader.GetValueAsync();
                                }
                                else if (element == "cost")
                                {
                                    drugPrice.Cost = double.Parse(await reader.GetValueAsync());
                                }
                                else if (element == "unit")
                                {
                                    drugPrice.Unit = await reader.GetValueAsync();
                                }
                            }

                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Depth == 2 && reader.Name == "drugbank-id")
                            {
                                drugPrimaryKey = false;
                            }

                            if (reader.Depth == 3 && reader.Name == "price")
                            {
                                parentElementPrice = false;
                                var temp = drugPrice.DeepClone();
                                drug.DrugProducts.Add(temp);
                            }

                            if (reader.Name == "drug" && reader.Depth == 1)
                            {
                                var temp = drug.DeepClone();
                                drugs.Add(temp);
                                await _uhpContext.Drugs.AddAsync(temp, cancellationToken);

                                drug = new Domain.Models.Drugs.Drug();
                                if (i == 1000)
                                {
                                    i = 0;
                                    await _uhpContext.SaveChangesAsync(cancellationToken);
                                }

                                i++;
                            }

                            break;
                    }
                }
            }

            await _uhpContext.SaveChangesAsync(cancellationToken);

            var firstDrugsId = new List<long>();
            var parentElementDrugInteraction = false;
            var drugInteractions = new Domain.Models.Drugs.DrugByDrugInteraction();
            using (var reader = XmlReader.Create(request.XmlFile.OpenReadStream(), settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            element = reader.Name;
                            depth = reader.Depth;
                            if (reader.Name == "drugbank-id" && depth == 2 && reader.HasAttributes)
                            {
                                drugPrimaryKey = Boolean.Parse(reader.GetAttribute("primary"));
                            }
                            else
                            {
                                drugPrimaryKey = false;
                            }

                            if (depth == 3 && reader.Name == "drug-interaction")
                            {
                                parentElementDrugInteraction = true;
                            }

                            break;
                        case XmlNodeType.Text:
                            if (depth == 2)
                            {
                                if (element == "drugbank-id" && drugPrimaryKey)
                                {
                                    var drugId = await reader.GetValueAsync();
                                    // var firstDrug = await _uhpContext.Drugs.FirstOrDefaultAsync(drug1 =>
                                    //     drug1.DrugBankId == drugId, cancellationToken);
                                    var firstDrug = drugs.Find(drug1 => drug1.DrugBankId == drugId);
                                    drugInteractions.FirstDrugId = firstDrug.Id;
                                }
                            }

                            if (depth == 4 && parentElementDrugInteraction)
                            {
                                if (element == "drugbank-id")
                                {
                                    var drugId = await reader.GetValueAsync();
                                    var secondDrug = drugs.Find(drug1 => drug1.DrugBankId == drugId);
                                    // // var secondDrug = await _uhpContext.Drugs.FirstOrDefaultAsync(drug1 =>
                                    //     drug1.DrugBankId == drugId, cancellationToken);
                                    if (secondDrug != null)
                                    {
                                        drugInteractions.SecondDrugId = secondDrug.Id;
                                    }
                                }
                                else if (element == "description")
                                {
                                    drugInteractions.Description = await reader.GetValueAsync();
                                }
                            }

                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Depth == 3 && reader.Name == "drug-interaction" &&
                                drugInteractions.SecondDrugId != 0 && !firstDrugsId.Contains(drugInteractions.SecondDrugId))
                            {
                                parentElementDrugInteraction = false;
                                var temp = drugInteractions.DeepClone();
                                firstDrugsId.Add(temp.FirstDrugId);
                                await _uhpContext.DrugByDrugInteractions.AddAsync(temp, cancellationToken);
                                drugInteractions = new Domain.Models.Drugs.DrugByDrugInteraction()
                                {
                                    FirstDrugId = temp.FirstDrugId
                                };
                            }

                            if (reader.Name == "drug" && reader.Depth == 1)
                            {
                                drugInteractions = new Domain.Models.Drugs.DrugByDrugInteraction();
                            }

                            break;
                    }
                }
            }

            await _uhpContext.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}