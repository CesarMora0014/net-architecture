using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Domain.Events;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Mappings;
public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Discount, DiscountDTO>().ReverseMap();
        CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();    

        // Si los campos son diferentes, se mapean de la siguiente manera
        //CreateMap<Customer, CustomerDTO>()
        //    .ReverseMap()
        //    .ForMember(destination => destination.CustomerID, source => source.MapFrom(src => src.CustomerID))
        //    .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName))
        //    .ForMember(destination => destination.ContactName, source => source.MapFrom(src => src.ContactName));


    }
}
