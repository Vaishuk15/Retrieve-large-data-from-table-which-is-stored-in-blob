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
        //public async Task<T> GetAsync(string partitionKey, string rowKey)
        //{
        //    TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
        //    TableResult result = await table.ExecuteAsync(retrieveOperation);
        //    return (T)result.Result;
        //}

        //public async Task DeleteAsync(string partitionKey, string rowKey)
        //{

        //    var entity = new TableEntity
        //    {
        //        PartitionKey = partitionKey,
        //        RowKey = rowKey,
        //        ETag = "*"
        //    };
        //    TableOperation delteOperation = TableOperation.Delete(entity);
        //    await table.ExecuteAsync(delteOperation);

        //}

        public async Task InsertAsync(T data)
        {
            //var tempType=typeof(T);
            //var genericArgument = typeof(T).GetGenericArguments().FirstOrDefault();
            //if (tempType != null && genericArgument != null)
            //{
            //    Type newType = tempType.MakeGenericType(genericArgument);
            //}
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
