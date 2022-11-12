using AutoMapper;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveUserResource, User>();
        CreateMap<SaveOrderResource, Order>();
        CreateMap<SaveOrderStatusResource, OrderStatus>();
        CreateMap<SaveProductResource, Product>();
        CreateMap<SaveCommentResource, Comment>();
        CreateMap<SaveNotificationsResource, Notifications>();
    }
}