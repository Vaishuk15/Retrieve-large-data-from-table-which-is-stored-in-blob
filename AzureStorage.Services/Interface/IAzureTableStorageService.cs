using AzureStorage.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Services.Interface
{
    public interface IAzureTableStorageService
    {
        Task<bool> AddToTable(SavedFormDemoViewModel jsonData);
    }
}
