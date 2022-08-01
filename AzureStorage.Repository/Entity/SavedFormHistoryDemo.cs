using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Entity
{
    public class SavedFormHistoryDemo : TableEntity
    {
        public SavedFormHistoryDemo(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public SavedFormHistoryDemo()
        {

        }
        public string Id { get; set; }
        public string FormId { get; set; }
        public string? Name { get; set; }
        public string FormJsonStringUrl { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
