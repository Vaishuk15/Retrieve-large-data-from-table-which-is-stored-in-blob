using AzureStorage.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Services.Interface
{
    public interface IAzureTableStorageService<T>
    {
        Task<bool> AddToTable(SavedFormDemoViewModel jsonData);
        Task<SavedFormDemoViewModel> GetTableData(string rowKey, string partitionKey);
    }
}
