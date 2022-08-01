using AutoMapper;
using AzureStorage.Models.ViewModel;
using AzureStorage.Repository.Entity;
using AzureStorage.Repository.Interface;
using AzureStorage.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Services.Implementation
{
    public class AzureTableStorageService<T> : IAzureTableStorageService<T> where T : class
    {
        private readonly IAzureTableStorageRepository<SavedFormHistoryDemo> _tableStorageRepository;
        private readonly IAzureBlobStorageRepository _blobStorageRepository;
        private readonly IMapper _mapper;

        public AzureTableStorageService(IAzureTableStorageRepository<SavedFormHistoryDemo> tableStorageRepository, IMapper mapper, IAzureBlobStorageRepository blobStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
            _blobStorageRepository = blobStorageRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddToTable(SavedFormDemoViewModel jsonData)
        {
            var savedForm = _mapper.Map<SavedFormHistoryDemo>(jsonData);
          var blobUri= await  _blobStorageRepository.AddToBlob(savedForm.RowKey, jsonData.FormJsonString);
            savedForm.FormJsonStringUrl = blobUri;
            var result =  _tableStorageRepository.InsertAsync(savedForm);

            return true;
        }


        public async Task<SavedFormDemoViewModel> GetTableData(string rowKey, string partitionKey)
        {
            var tableData = await _tableStorageRepository.GetAsync(partitionKey, rowKey);
            //var tableData=  _tableStorageRepository.GetByPartitionKey(partitionKey);
            var table = _mapper.Map<SavedFormHistoryDemo>(tableData);
            var data = _mapper.Map<SavedFormDemoViewModel>(tableData);
            data.FormJsonString = await _blobStorageRepository.GetBlobData(table.FormJsonStringUrl, table.RowKey);
            return data;
        }
    }
}
