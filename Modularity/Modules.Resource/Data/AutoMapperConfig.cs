using AutoMapper;
using Modules.Resource.Entities;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.Data
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Menu, MenuItem>().ReverseMap();
        }
    }
}
