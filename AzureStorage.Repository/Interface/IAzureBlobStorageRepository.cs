using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Interface
{
    public interface IAzureBlobStorageRepository
    {
        Task<string> AddToBlob(string fileName, string jsonString);
        public Uri GetServiceSasUriForContainer(string fileName);
        Task<string> GetBlobData(string blobUrl, string fileName);
    }
}
