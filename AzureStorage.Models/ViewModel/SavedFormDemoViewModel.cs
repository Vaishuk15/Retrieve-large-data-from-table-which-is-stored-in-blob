using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Models.ViewModel
{
    public class SavedFormDemoViewModel
    {
        public string Id { get; set; }
        public string FormId { get; set; }
        public string? Name { get; set; }
        public string? FormJsonString { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
