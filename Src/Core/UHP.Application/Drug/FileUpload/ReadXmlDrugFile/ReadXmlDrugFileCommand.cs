using MediatR;
using Microsoft.AspNetCore.Http;

namespace UHP.Application.Drug.FileUpload.ReadXmlDrugFile
{
    public class ReadXmlDrugFileCommand : IRequest
    {
        public IFormFile XmlFile { get; set; }

    }
}