using AutoMapper;
using FastCreditChallenge.Entities;
using FastCreditChallenge.Utilities.Dtos.Request;
using FastCreditChallenge.Utilities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Core.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
