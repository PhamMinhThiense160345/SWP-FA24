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
            CreateMap<Comment, CommentView>().ReverseMap();

        }
    }
}
