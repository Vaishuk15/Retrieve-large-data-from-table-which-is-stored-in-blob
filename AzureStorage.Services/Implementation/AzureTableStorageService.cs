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
    public class AzureTableStorageService : IAzureTableStorageService
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
            _blobStorageRepository.AddToBlob(jsonData.FormJsonString);
            var result = _tableStorageRepository.InsertAsync(_mapper.Map<SavedFormHistoryDemo>(jsonData));
            return true;
        }
    }
}
