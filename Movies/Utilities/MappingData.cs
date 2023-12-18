using AutoMapper;
using Movies.Business;
using Movies.Models;

namespace Movies.Utilities
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            //movie
            CreateMap<Movie, MoviePreview>();

            CreateMap<Movie, MovieDetail>()
                .ForMember(dest => dest.CastCharacteries,
                opt => opt.MapFrom(src => src.Casts))
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories));

            CreateMap<Cast, CastCharacter>()
                .ForMember(dest => dest.NameActor, 
                opt => opt.MapFrom(src => src.Actor.NameActor))
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.Actor.Image));

            CreateMap<MovieCategory, Category>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<MovieCategory, MoviePreview>();

            //actor
            CreateMap<Actor, ActorDetail>()
                .ForMember(dest => dest.NationName,
                opt => opt.MapFrom(src => src.Nation.Name));
            CreateMap<ActorDetail, Actor>();
        }
    }
}
