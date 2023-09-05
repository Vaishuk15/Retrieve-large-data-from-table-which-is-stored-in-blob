using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using AzureStorage.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Implementation
{
    public class AzureBlobStorageRepository : IAzureBlobStorageRepository
    {
        private readonly BlobContainerClient _formBuilderContainerClient;
        private readonly string _formBuilderGpmdConnection;
        private readonly string _containerName;
        public AzureBlobStorageRepository(IConfiguration configuration)
        {


            _formBuilderGpmdConnection = configuration.GetConnectionString("BlobStorageConnection") ?? Environment.GetEnvironmentVariable("BlobStorageConnection");
            _containerName = configuration["SavedFormHistoryContainer"];

            _formBuilderContainerClient = GetContainerClient(_formBuilderGpmdConnection, _containerName);

        }


        public async Task<string> AddToBlob(string fileName,string jsonString)
        {


            // Retrieve reference to a blob named.
            BlobClient blockBlob = _formBuilderContainerClient.GetBlobClient(fileName);


            var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/json" };



            using (MemoryStream json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                await blockBlob.UploadAsync(json, blobHttpHeader);
            }
            return blockBlob.Uri.ToString();
        }

        public Uri GetServiceSasUriForContainer(string fileName)
        {
            // Check whether this BlobContainerClient object has been authorized with Shared Key.
            if (!_formBuilderContainerClient.CanGenerateSasUri)
            {
                return null;
            }

            // Create a SAS token that's valid for one hour.
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _containerName,
                Resource = "c"
            };

            sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(15);
            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.List | BlobContainerSasPermissions.Tag);

            Uri sasUri = _formBuilderContainerClient.GenerateSasUri(sasBuilder);
            return sasUri;
        }

        public async Task<string> GetBlobData(string blobUrl,string fileName)
        {
           // var sasUrl = GetServiceSasUriForContainer(blobUrl).ToString();
           //var url= sasUrl.Split("?");
           // var res = url[0]+"/"+ fileName + "?" + url[1];
            BlobClient blockBlob = _formBuilderContainerClient.GetBlobClient(fileName);
            BlobDownloadResult downloadResult = await blockBlob.DownloadContentAsync();
            string downloadedData = downloadResult.Content.ToString();
            return downloadedData;
        }

        private BlobContainerClient GetContainerClient(string connectionString, string containerName)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            bool isExist = containerClient.Exists();
            if (!isExist)
            {
                containerClient.Create();
            }

            return containerClient;
        }
    }
}
