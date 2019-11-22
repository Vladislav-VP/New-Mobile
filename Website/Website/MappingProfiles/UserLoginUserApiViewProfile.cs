using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Api;

namespace MappingProfiles
{
    public class UserLoginUserApiViewProfile : Profile
    {
        public UserLoginUserApiViewProfile()
        {
            CreateMap<User, LoginUserApiView>()
                    .ForMember(vm => vm.Id, map => map.MapFrom(entity => entity.Id))
                    .ForMember(vm => vm.Name, map => map.MapFrom(entity => entity.Name))
                    .ForMember(vm => vm.Password, map => map.MapFrom(entity => entity.Password));
        }
    }
}
