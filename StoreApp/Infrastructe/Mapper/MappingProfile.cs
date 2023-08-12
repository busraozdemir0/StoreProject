using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace StoreApp.Infrastructe.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion, Product>(); //Mapleme işlemi otomatik olarak gerçekleşerek dto, Product nesnesine dönüştürülecek
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap(); 
        }
    }
}