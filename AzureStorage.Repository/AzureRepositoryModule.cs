using AzureStorage.Repository.Entity;
using AzureStorage.Repository.Implementation;
using AzureStorage.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Repository
{
    public class AzureRepositoryModule
    {
        public AzureRepositoryModule(IServiceCollection services)
        {
            services.AddTransient<IAzureBlobStorageRepository, AzureBlobStorageRepository>();
            services.AddTransient(typeof(IAzureTableStorageRepository<>), typeof(AzureTableStorageRepository<>));
        }
    }
}
