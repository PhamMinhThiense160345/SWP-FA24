using AutoMapper;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<Article, ArticleView>().ReverseMap();
            CreateMap<Comment, CommentView>().ReverseMap();
            CreateMap<User, StaffView>().ReverseMap();
            CreateMap<Dish, DishView>().ReverseMap();
            CreateMap<Feedback, FeedbackView>().ReverseMap();

        }
    }
}
