using AutoMapper;
using Modules.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Data
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Entities.User,UserDto>().ReverseMap();
        }
    }
}
