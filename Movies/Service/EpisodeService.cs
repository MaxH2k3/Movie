using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Movies.Repository
{
    public class EpisodeService : IEpisodeRepository
    {
        private readonly MOVIESContext _context;
        private readonly IMapper _mapper;

        public EpisodeService(MOVIESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public EpisodeService(IMapper mapper)
        {
            _context = new MOVIESContext();
            _mapper = mapper;
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _context.Episodes.Include(e => e.Season);
        }

        public IEnumerable<Episode> GetEpisodesBySeason(Guid seasonId)
        {
            return GetEpisodes().Where(e => e.SeasonId.Equals(seasonId)).OrderBy(e => e.EpisodeNumber).ToList();
        }
        /*
        public ResponseDTO CreateEpisode(NewEpisode newEpisode)
        {
            var season = _context.Seasons.FirstOrDefault(s => s.SeasonId.Equals(newEpisode.SeasonId));
            if(season == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found");
            }

            var episodeId = Guid.NewGuid();
            int episodeNumber = GenerateEpisodeNumber((Guid)newEpisode.SeasonId);
            try
            {
                Episode episode = new Episode()
                {
                    EpisodeId = episodeId,
                    SeasonId = newEpisode.SeasonId,
                    EpisodeNumber = episodeNumber,
                    Name = newEpisode.Name,
                    Video = newEpisode.Video,
                    DateCreated = (newEpisode.DateCreated == null) ? DateTime.Now : newEpisode.DateCreated,
                };
                _context.Episodes.Add(episode);
                if (_context.SaveChanges() > 0)
                {
                    return new ResponseDTO(HttpStatusCode.Created, "Create Episode Successfully!", episodeId);
                }
            } catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return new ResponseDTO(HttpStatusCode.BadRequest, innerException?.Message, newEpisode.Video);
            }
            
            return new ResponseDTO(HttpStatusCode.BadRequest, "Server database Error! Fail while saving data.");
        }

        */

        public Episode CreateEpisode(NewEpisode newEpisode, int episodeNumber, Guid seasonId)
        {

            var episodeId = Guid.NewGuid();
            Episode episode = new Episode()
            {
                EpisodeId = episodeId,
                SeasonId = seasonId,
                EpisodeNumber = episodeNumber,
                Name = newEpisode.Name,
                Video = newEpisode.Video,
                DateCreated = (newEpisode.DateCreated == null) ? DateTime.Now : newEpisode.DateCreated,
            };

            return episode;
        }
        
        /*
        public IEnumerable<ResponseDTO> CreateEpisodes(IEnumerable<NewEpisode> newEpisodes)
        {
            IEnumerable<ResponseDTO> responses = new List<ResponseDTO>();
            int count = newEpisodes.Count();
            for(int i = 0; i < count; i++)
            {
                //int episodeNumber = GenerateEpisodeNumber(seasonId) + i;
                responses = responses.Append(CreateEpisode(newEpisodes.ElementAt(i)));
            }
            return responses;
        }
        */

        public async Task<ResponseDTO> CreateEpisodes(IEnumerable<NewEpisode> newEpisodes, Guid seasonId)
        {
            var season = await _context.Seasons.FindAsync(seasonId);
            if(season == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found");
            }

            int count = newEpisodes.Count();
            LinkedList<Episode> episodes = new LinkedList<Episode>();

            for(int i = 0; i < count; i++)
            {
                var episode = newEpisodes.ElementAt(i);
                int episodeNumber = GenerateEpisodeNumber(seasonId) + i;
                episodes.AddLast(CreateEpisode(newEpisodes.ElementAt(i), episodeNumber, seasonId));
            }

            await _context.Episodes.AddRangeAsync(episodes);
            
            if(await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.Created, "Create episode successfully!", season.MovieId);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server error!");
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
                if (await _context.SaveChangesAsync() > 0)
                {
                    var check = await CheckNumber((Guid) episode.SeasonId, (int) episode.EpisodeNumber);
                    if(check)
                    {
                        return new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!");
                    }
                    return new ResponseDTO(HttpStatusCode.InternalServerError, "Error When checking episode number!");
                }
                return new ResponseDTO(HttpStatusCode.ServiceUnavailable, "Server Database Error!");
            }
            return new ResponseDTO(HttpStatusCode.NotFound, "Season Not Found!", $"EpisodeId: {episodeId}");
        }

        public async Task<ResponseDTO> DeleteEpisodeBySeason(Guid seasonId)
        {
            var episodes = _context.Episodes.Where(s => s.SeasonId.Equals(seasonId)).OrderBy(e => e.EpisodeNumber).ToList();
            /*if (episodes.Count() > 0)
            {
                episodes.ToList().ForEach(e =>
                {
                    Guid? id = e.EpisodeId;
                    _context.Episodes.Remove(e);
                    if (_context.SaveChanges() > 0)
                    {
                        responses = responses.Append(new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!", id));

                    } else
                    {
                        responses = responses.Append(new ResponseDTO(HttpStatusCode.NotModified, "Fail to delete episode", id));

                    }

                });
            }*/
            if(episodes.Count() <= 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!", seasonId);
            }

            _context.Episodes.RemoveRange(episodes);
            
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Delete Episode Successfully!", seasonId);
            }

            return new ResponseDTO(HttpStatusCode.InternalServerError, "Server Error!");
        }

        public async Task<ResponseDTO> UpdateEpisode(NewEpisode newEpisode, Guid episodeId)
        {
            var episode = GetEpisode(episodeId);
            if (episode != null)
            {
                episode.Name = newEpisode.Name;
                episode.Video = newEpisode.Video;
                episode.DateCreated = (newEpisode.DateCreated == null) ? episode.DateCreated : newEpisode.DateCreated;
                episode.DateUpdated = DateTime.Now;
                _context.Episodes.Update(episode);
                if (_context.SaveChanges() > 0)
                {
                    return new ResponseDTO(HttpStatusCode.OK, "Update Episode Successfully!", episodeId);
                }
                return new ResponseDTO(HttpStatusCode.NotModified, "Fail to update episode", episodeId);
            }
            return new ResponseDTO(HttpStatusCode.NotFound, "Episode Not Found!", $"EpisodeId: {episodeId}");
        }

        public async Task<bool> CheckNumber(Guid seasonId, int episodeNumber)
        {
            var episodes = _context.Episodes.Where(e => e.SeasonId.Equals(seasonId)).OrderBy(e => e.EpisodeNumber).ToList();
            int count = episodes.Count();
            if (count <= 0)
            {
                return true;
            }
            for(int i = episodeNumber - 1; i < count; i++)
            {
                //episodes[i].EpisodeNumber = episodes[i].EpisodeNumber - 1;
                episodes[i].EpisodeNumber = i + 1;
            }
            _context.Episodes.UpdateRange(episodes);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        //1 2 3 4 5
        // 1 3 4 5 => 4, i = 1
        // 1 2 3 4
        //count = 5

        public async Task<ResponseDTO> UpdateEpisodes(Guid seasonId, IEnumerable<EpisodeDTO> newEpisodes)
        {
            LinkedList<Episode> episodes = new LinkedList<Episode>();

            for (int i = 0; i < newEpisodes.Count(); i++)
            {
                episodes.AddLast(new Episode()
                {
                    EpisodeId = newEpisodes.ElementAt(i).EpisodeId,
                    SeasonId = seasonId,
                    EpisodeNumber = i + 1,
                    Name = newEpisodes.ElementAt(i).Name,
                    Video = newEpisodes.ElementAt(i).Video,
                    DateCreated = newEpisodes.ElementAt(i).DateCreated,
                    DateUpdated = DateTime.Now
                });
            }

            //get episode by seasonId
            //var existEpisodes = GetEpisodesBySeason(seasonId).ToList();

            //delete episode not in newEpisodes
            //var episodesToDelete = existEpisodes.Where(e => !episodes.Any(ep => ep.EpisodeId == e.EpisodeId)).ToList();
            //_context.Episodes.RemoveRange(episodesToDelete);

            //update episode in newEpisodes
            _context.Episodes.UpdateRange(episodes);

            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseDTO(HttpStatusCode.OK, "Update Episode Successfully!");
            }
            
            return new ResponseDTO(HttpStatusCode.InternalServerError, "Serer Error!");
        }

    }
}
