using AzureStorage.Models.ViewModel;
using AzureStorage.Repository.Entity;
using AzureStorage.Repository.Interface;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository.Implementation
{
    public class AzureTableStorageRepository<T> : IAzureTableStorageRepository<T> where T : ITableEntity, new()
    {
        private readonly string _tableStorageConnection;
        private readonly string _tableName;
        private readonly CloudTable table;


        public AzureTableStorageRepository(IConfiguration configuration)
        {
            _tableStorageConnection = configuration.GetConnectionString("TableStorageConnection") ?? Environment.GetEnvironmentVariable("TableStorageConnection");
            _tableName = typeof(T).Name;
            table = CreateTableAsync(_tableName);
        }
        public async Task<T> GetAsync(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            return (T)result.Result;
        }

        public  T GetByPartitionKey(string partitionKey)
        {
            TableQuery<T> rangeQuery = new TableQuery<T>().Where(
                        TableQuery.GenerateFilterCondition("FormId", QueryComparisons.Equal, partitionKey));

            var results = new List<T>();

            TableContinuationToken continuationToken = null;
            do
            {
                var data = table.ExecuteQuerySegmented(rangeQuery, continuationToken);
                continuationToken = data.ContinuationToken;
                results.AddRange(data.Results);

            } while (continuationToken != null);

            var response = results.OrderByDescending(x => x.Timestamp).FirstOrDefault();
            return response;
        }

        public async Task InsertAsync(T data)
        {
                TableOperation insertOrMergeOperation = TableOperation.Insert(data);

            await table.ExecuteAsync(insertOrMergeOperation);
        }
        private CloudTable CreateTableAsync(string tableName)
        {
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(_tableStorageConnection);

            CloudTableClient tableClient =
                storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            CloudTable tableAsync = tableClient.GetTableReference(tableName);
            tableAsync.CreateIfNotExistsAsync();

            return tableAsync;
        }

        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            return CloudStorageAccount.Parse(storageConnectionString);
        }
    }
}
