using System.Collections.Generic;

namespace UHP.Application.Models
{
    public class DrugsListViewModel
    {
        public List<DrugViewModel> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Length { get; set; }
    }
}