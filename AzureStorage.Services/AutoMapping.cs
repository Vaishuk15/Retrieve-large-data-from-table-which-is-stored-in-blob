using AutoMapper;
using AzureStorage.Models.ViewModel;
using AzureStorage.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SavedFormDemoViewModel, SavedFormHistoryDemo>()
                .ForMember(d => d.PartitionKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.RowKey, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(d => d.FormId, opt => opt.MapFrom(src => src.FormId))
                .ReverseMap();
        }
    }
}
