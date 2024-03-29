﻿using AutoMapper;
using MongoDB.Bson;
using Movies.Business.movies;
using Movies.Business.persons;
using Movies.Business.seasons;
using Movies.Business.users;
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

            CreateMap<Movie, MovieNewest>()
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories));

            CreateMap<Movie, NewMovie>();
            CreateMap<NewMovie, Movie>();
            CreateMap<Movie, MovieRecommend>();

            //Person
            CreateMap<Cast, CastCharacter>()
                .ForMember(dest => dest.PersonId,
                opt => opt.MapFrom(src => src.Actor.PersonId))
                .ForMember(dest => dest.NamePerson,
                opt => opt.MapFrom(src => src.Actor.NamePerson))
                .ForMember(dest => dest.Thumbnail,
                opt => opt.MapFrom(src => src.Actor.Thumbnail));

            CreateMap<Person, PersonDetail>()
                .ForMember(dest => dest.NationName,
                opt => opt.MapFrom(src => src.Nation.Name));
            CreateMap<PersonDetail, Person>();

            CreateMap<Person, PersonDTO>()
                .ForMember(dest => dest.NationName,
                opt => opt.MapFrom(src => src.Nation.Name));

            CreateMap<Person, NewPerson>();

            CreateMap<NewPerson, Person>();

            //season
            CreateMap<Season, SeasonDTO>();

            CreateMap<Episode, EpisodeDTO>();

            //user
            CreateMap<RegisterUser, User>();
            CreateMap<User, RegisterUser>();
            CreateMap<UserTemporary, User>();
            CreateMap<User, UserDetail>();
        }
    }
}
