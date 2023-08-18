using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion, Product>(); //Mapleme işlemi otomatik olarak gerçekleşerek dto, Product nesnesine dönüştürülecek
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap(); 
            CreateMap<UserDtoForCreation, IdentityUser>(); 
            CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap(); 
            CreateMap<CategoryDtoForInsertion, Category>(); 
            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
        }
    }
}