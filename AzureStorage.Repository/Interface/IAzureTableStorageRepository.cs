using AzureStorage.Models.ViewModel;
using AzureStorage.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Interface
{
    public interface IAzureTableStorageRepository<T>
    {
        //Task<T> GetAsync(string partitionKey, string rowKey);
        //Task DeleteAsync(string partitionKey, string rowKey);
        Task InsertAsync(T data);
    }
}
