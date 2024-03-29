﻿using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using System.Net;

namespace Movies.Repository
{
    public class SeasonService : ISeasonService
    {
        private readonly MOVIESContext _context;
        private readonly IMovieService _movieService;
        private readonly IEpisodeService _episodeService;

        public SeasonService(MOVIESContext context, IMovieService movieRepository, 
                        IEpisodeService episodeRepository)
        {
            _context = context;
            _movieService = movieRepository;
            _episodeService = episodeRepository;
        }

        public SeasonService(IMovieService movieRepository, IEpisodeService episodeRepository)
        {
            _context = new MOVIESContext();
            _movieService = movieRepository;
            _episodeService = episodeRepository;
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _context.Seasons.Include(s => s.Episodes);
        }

        public IEnumerable<Season> GetSeasonsByMovie(Guid movieId)
        {
            return GetSeasons().Where(s => s.MovieId.Equals(movieId)).ToList()
                .Select(s =>
                {
                    s.Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).ToList();
                    return s;
                });
        }

        public IEnumerable<Season> GetSeasonsByMovieAndNumber(Guid movieId, int? seasonNumber)
        {
            return GetSeasons().Where(s => s.MovieId.Equals(movieId) && s.SeasonNumber == seasonNumber).ToList()
                .Select(s =>
                {
                    s.Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).ToList();
                    return s;
                });
        }

        //public async Task<ResponseDTO> CreateSeason(NewSeason newSeason)
        //{
        //    int? featureId = _movieService.GetFeatureIdByMovieId((Guid)newSeason.MovieId);
        //    Guid? seasonId = Guid.NewGuid();
        //    if(newSeason.SeasonId == null)
        //    {
        //        var seasonNumber = GenerateSeasonNumber((Guid)newSeason.MovieId);
        //        Season season = new Season()
        //        {
        //            SeasonId = seasonId,
        //            SeasonNumber = seasonNumber,
        //            MovieId = (Guid)newSeason.MovieId,
        //            Name = (newSeason.Name == null) ? ("Season " + seasonNumber) : newSeason.Name,
        //        };
        //        _context.Seasons.Add(season);
        //        if(await _context.SaveChangesAsync() == 0)
        //        {
        //            return new ResponseDTO(HttpStatusCode.BadRequest, "Server database Error! Fail while saving data.");
        //        }
        //    }
        //    //save list episode
        //    var responseDTO = _episodeService.CreateEpisodes(newSeason.Episodes,
        //                                ((newSeason.SeasonId == null) ? (Guid)seasonId : (Guid)newSeason.SeasonId));

        //    return new ResponseDTO(HttpStatusCode.Created, "Create Season Successfully!", responseDTO);
        //}



        public int GenerateSeasonNumber(Guid movieId)
        {
            var seasons = GetSeasonsByMovie(movieId);
            return seasons.Count() + 1;
        }

        public Season? GetSeason(Guid seasonId)
        {
            return GetSeasons().FirstOrDefault(s => s.SeasonId.Equals(seasonId));
        }

        public async Task<ResponseDTO> DeleteSeason(Guid seasonId)
        {
            var response = await _episodeService.DeleteEpisodeBySeason(seasonId);
            if (response.Status != HttpStatusCode.OK)
            {
                return response;
            }

            var season = GetSeason(seasonId);
            if(season != null)
            {
                _context.Seasons.Remove(season);
                if(await _context.SaveChangesAsync() >= 0)
                {
                    var check  = await CheckSeasonNumber((Guid)season.MovieId, (int)season.SeasonNumber);
                    if(check)
                    {
                        return new ResponseDTO(HttpStatusCode.OK, "Delete Season Successfully!");
                    }
                    return new ResponseDTO(HttpStatusCode.InternalServerError, "Error When checking season number!");
                }
                return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server Database Error!");
            }
            return new ResponseDTO(HttpStatusCode.NotFound, "Not found your season!");
        }

        public async Task<bool> CheckSeasonNumber(Guid movieId, int seasonNumber)
        {
            var seasons = _context.Seasons.Where(s => s.MovieId.Equals(movieId)).OrderBy(s => s.SeasonNumber).ToList();
            int count = seasons.Count();
            if(count <= 0)
            {
                return true;
            }
            for(int i = seasonNumber - 1; i < count; i++)
            {
                seasons[i].SeasonNumber = i + 1;
            }
            _context.Seasons.UpdateRange(seasons);
            if(await _context.SaveChangesAsync() >= 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ResponseDTO> CreateSeason(NewSeason newSeason)
        {
            var movie = _movieService.GetMovieById((Guid)newSeason.MovieId);
            if(movie == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Movie Not Found!", newSeason.MovieId);
            }

            Guid id = Guid.NewGuid();
            Season season = new Season()
            {
                SeasonId = id,
                MovieId = (Guid)newSeason.MovieId,
                SeasonNumber = GenerateSeasonNumber((Guid)newSeason.MovieId),
                Name =  newSeason.Name
            };

            await _context.Seasons.AddAsync(season);
            if(await _context.SaveChangesAsync() >= 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create Season Successfully!", id);
                
            }
            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server Database Error!");
        }

        public async Task<ResponseDTO> UpdateSeason(string? name, Guid seasonId)
        {

            var season = GetSeason(seasonId);
            if(season == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found!", seasonId);
            }

            season.Name = name;

            _context.Seasons.Update(season);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server Database Error!");
            }
            return new ResponseDTO(HttpStatusCode.OK, "Update Season Successfully!");
        }

        public async Task<ResponseDTO> DeleteSeasonByMovie(Guid id)
        {
            var seasons = GetSeasonsByMovie(id);

            if(seasons.Count() <=  0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Delete successfully!");
            }

            foreach(var season in seasons)
            {
                var response = await _episodeService.DeleteEpisodeBySeason((Guid)season.SeasonId);
                if (response.Status != HttpStatusCode.OK)
                {
                    return response;
                }
            }

            seasons = GetSeasonsByMovie(id);
            _context.Seasons.RemoveRange(seasons);
            if (await _context.SaveChangesAsync() >= 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Delete Successffully!");
            }

            return new ResponseDTO(HttpStatusCode.InternalServerError, "Server Error!");
        }

        public int GetTotalSeason(Guid movieId)
        {
            return _context.Seasons.Where(s => s.MovieId.Equals(movieId)).Count();
        }

    }
}
