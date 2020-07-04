using AutoMapper;
using ManagementSystem.Models;
using ManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<IoTDevice, IoTDeviceViewModel>();
            CreateMap<AddDeviceVM, IoTDevice>();
            
        }
    }
}
