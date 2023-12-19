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
            CreateMap<Movie, MoviePreview>()
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories));

            CreateMap<Movie, MovieDetail>()
                .ForMember(dest => dest.CastCharacteries,
                opt => opt.MapFrom(src => src.Casts))
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories));

            CreateMap<MovieCategory, Category>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<MovieCategory, MoviePreview>();

            CreateMap<Movie, MovieNewest>();

            //actor
            CreateMap<Cast, CastCharacter>()
                .ForMember(dest => dest.PersonId,
                opt => opt.MapFrom(src => src.Actor.PersonId))
                .ForMember(dest => dest.NamePerson,
                opt => opt.MapFrom(src => src.Actor.NamePerson))
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.Actor.Image));

            CreateMap<Person, PersonDetail>()
                .ForMember(dest => dest.NationName,
                opt => opt.MapFrom(src => src.Nation.Name));
            CreateMap<PersonDetail, Person>();

            CreateMap<Person, PersonDTO>();

            //season
            CreateMap<Season, SeasonDTO>();

            CreateMap<Episode, EpisodeDTO>();

        }
    }
}
