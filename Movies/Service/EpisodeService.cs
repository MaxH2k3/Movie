using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;
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

        public IEnumerable<Episode> GetEpisodesBySeason(Guid seasonId)
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

            var episodeId = Guid.NewGuid();
            int episodeNumber = GenerateEpisodeNumber(seasonId);
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
                    return new ResponseDTO(HttpStatusCode.Created, "Create Episode Successfully!", $"EpisodeId: {episodeId}, EpisodeNumber: {episodeNumber}");
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
            int count = newEpisodes.Count();
            for(int i = 0; i < count; i++)
            {
                //int episodeNumber = GenerateEpisodeNumber(seasonId) + i;
                responses = responses.Append(CreateEpisode(newEpisodes.ElementAt(i), seasonId));
            }
            return responses;
        }


        public int GenerateEpisodeNumber(Guid seasonId)
        {
            var episodes = GetEpisodesBySeason(seasonId);
            return episodes.Count() + 1;
        }

        public Episode? GetEpisode(Guid episodeId)
        {
            return GetEpisodes().FirstOrDefault(e => e.EpisodeId.Equals(episodeId));
        }

        public async Task<ResponseDTO> DeleteEpisode(Guid episodeId)
        {
            var episode = GetEpisode(episodeId);
            if (episode != null)
            {
                _context.Episodes.Remove(episode);
                if (await _context.SaveChangesAsync() == 0)
                {
                    return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server Database Error!");
                }
                return new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!");
            }
            return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found!", $"EpisodeId: {episodeId}");
        }

        public IEnumerable<ResponseDTO> DeleteEpisodeBySeason(Guid seasonId)
        {
            IEnumerable<ResponseDTO> responses = new List<ResponseDTO>();
            var episodes = _context.Episodes.Where(s => s.SeasonId.Equals(seasonId)).ToList();
            if (episodes.Count() > 0)
            {
                episodes.ToList().ForEach(e =>
                {
                    Guid? id = e.EpisodeId;
                    _context.Episodes.Remove(e);
                    if (_context.SaveChanges() > 0)
                    {
                        responses = responses.Append(new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!", $"EpisodeID: {id}"));

                    } else
                    {
                        responses = responses.Append(new ResponseDTO(HttpStatusCode.NotModified, "Fail to delete episode", $"EpisodeId: {id}"));

                    }

                });
            }
            return responses;
        }
    }
}
