using AzureStorage.Services.Implementation;
using AzureStorage.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Services
{
    public class ServiceModule
    {
        public ServiceModule(IServiceCollection services)
        {
           services.AddTransient<IAzureBlobStorageService, AzureBlobStorageService>();
            //services.AddTransient<IAzureTableStorageService, AzureTableStorageService>();
            services.AddTransient(typeof(IAzureTableStorageService<>), typeof(AzureTableStorageService<>));

        }
    }
}
