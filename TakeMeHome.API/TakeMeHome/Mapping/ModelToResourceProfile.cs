using AutoMapper;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
       CreateMap<User, UserResource>();
       CreateMap<Order, OrderResource>();
       CreateMap<OrderStatus, OrderStatusResource>();
       CreateMap<Product, ProductResource>();
       CreateMap<Comment, CommentResource>();
       CreateMap<Notifications, NotificationsResource>();
    }
}