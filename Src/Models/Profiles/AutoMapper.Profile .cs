using AutoMapper;
using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Models.Profiles
{
   public class AutoMapperProfile : Profile
   {
      public AutoMapperProfile()
      {
         CreateMap<TokenUserModelDto, UserModel>();
         CreateMap<UserModelDto, UserModel>();
         CreateMap<PersonModelDTO, PersonModel>();
      }
   }
}
