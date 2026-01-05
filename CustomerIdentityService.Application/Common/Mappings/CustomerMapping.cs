using AutoMapper;
using CustomerIdentityService.Core;
using CustomerIdentityService.Core.Dtos.Customers;
using CustomerIdentityService.Core.Dtos.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.Common.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>()
                 .ForMember(
                     dest => dest.Username,
                     opt => opt.MapFrom(src =>
                         !string.IsNullOrEmpty(src.Email)
                             ? src.Email
                             : src.PhoneNumber));
            CreateMap<Customer, CustomerDto>();


            CreateMap<GoogleUserInfoDto, CustomerDto>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.Email)
            )
            .ForMember(
                dest => dest.DisplayName,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.AvatarUrl,
                opt => opt.MapFrom(src => src.Picture)
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)
            );
        }
    }
}
