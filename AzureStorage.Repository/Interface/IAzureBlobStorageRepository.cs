using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Interface
{
    public interface IAzureBlobStorageRepository
    {
        Task AddToBlob(string jsonString);
    }
}
