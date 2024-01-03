using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using Movies.Utilities;
using System.Net;

namespace Movies.Repository
{
    public class SeasonService : ISeasonRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMovieRepository _movieService;
        private readonly IEpisodeRepository _episodeService;

        public SeasonService(MOVIESContext context, IMovieRepository movieRepository, 
                        IEpisodeRepository episodeRepository)
        {
            _context = context;
            _movieService = movieRepository;
            _episodeService = episodeRepository;
        }

        public SeasonService(IMovieRepository movieRepository, IEpisodeRepository episodeRepository)
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

        public async Task<ResponseDTO> CreateSeason(NewSeason newSeason)
        {
            int? featureId = _movieService.GetFeatureIdByMovieId((Guid)newSeason.MovieId);
            Guid? seasonId = Guid.NewGuid();
            if(newSeason.SeasonId == null)
            {
                var seasonNumber = GenerateSeasonNumber((Guid)newSeason.MovieId);
                Season season = new Season()
                {
                    SeasonId = seasonId,
                    SeasonNumber = seasonNumber,
                    MovieId = (Guid)newSeason.MovieId,
                    Name = (newSeason.Name == null) ? ("Season " + seasonNumber) : newSeason.Name,
                };
                _context.Seasons.Add(season);
                if(await _context.SaveChangesAsync() == 0)
                {
                    return new ResponseDTO(HttpStatusCode.BadRequest, "Server database Error! Fail while saving data.");
                }
            }
            //save list episode
            var responseDTO = _episodeService.CreateEpisodes(newSeason.Episodes,
                                        ((newSeason.SeasonId == null) ? (Guid)seasonId : (Guid)newSeason.SeasonId));

            return new ResponseDTO(HttpStatusCode.Created, "Create Season Successfully!", responseDTO);
        }

        public int GenerateSeasonNumber(Guid movieId)
        {
            var seasons = GetSeasonsByMovie(movieId);
            return seasons.Count() + 1;
        }

    }
}
