using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using System.Net;

namespace Movies.Repository
{
    public class EpisodeService : IEpisodeRepository
    {
        private readonly MOVIESContext _context;

        public EpisodeService(MOVIESContext context)
        {
            _context = context;
        }

        public EpisodeService()
        {
            _context = new MOVIESContext();
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _context.Episodes.Include(e => e.Season);
        }

        public IEnumerable<Episode> GetEpisodesBySeason(string seasonId)
        {
            return GetEpisodes().Where(e => e.SeasonId.Equals(seasonId)).ToList();
        }

        public ResponseDTO CreateEpisode(NewEpisode newEpisode, Guid seasonId)
        {
            var season = _context.Seasons.FirstOrDefault(s => s.SeasonId.Equals(seasonId));
            if(season == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found");
            }

            var episodeNumber = GenerateEpisodeNumber(seasonId);
            var episodeId = Guid.NewGuid();
            try
            {
                Episode episode = new Episode()
                {
                    EpisodeId = episodeId,
                    SeasonId = seasonId,
                    EpisodeNumber = episodeNumber,
                    Name = (newEpisode.Name == null) ? ("Episode " + episodeNumber) : newEpisode.Name,
                    Video = newEpisode.Video,
                    DateCreated = DateTime.Now
                };
                _context.Episodes.Add(episode);
                if (_context.SaveChanges() > 0)
                {
                    return new ResponseDTO(HttpStatusCode.Created, "Create Episode Successfully!", $"EpisodeId: {episodeId}\n, EpisodeNumber: {episodeNumber}");
                }
            } catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return new ResponseDTO(HttpStatusCode.BadRequest, innerException?.Message, newEpisode.Video);
            }
            
            return new ResponseDTO(HttpStatusCode.BadRequest, "Server database Error! Fail while saving data.");
        }

        public IEnumerable<ResponseDTO> CreateEpisodes(IEnumerable<NewEpisode> newEpisodes, Guid seasonId)
        {
            IEnumerable<ResponseDTO> responses = new List<ResponseDTO>();
            newEpisodes.ToList().ForEach(e =>
            {
                responses.Append(CreateEpisode(e, seasonId));
                
            });
            return responses;
        }


        public int? GenerateEpisodeNumber(Guid seasonId)
        {
            var episodes = GetEpisodesBySeason(seasonId.ToString());
            return episodes.Count() + 1;
        }
    }
}
