using Azure.Storage.Blobs;
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


        public async Task AddToBlob(string jsonString)
        {

            BlobClient blob = _formBuilderContainerClient.GetBlobClient("SavedFormHistoryDemo");

            using (MemoryStream json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                await blob.UploadAsync(json);
            }
            /*BlobContainerClient.UploadBlobAsync(json);*/
        }

        public Uri getBlobUrl()
        {

            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _formBuilderContainerClient.Name,
                Resource = "c"
            };
            Uri sasUri = _formBuilderContainerClient.GenerateSasUri(sasBuilder);
            return sasUri;
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
