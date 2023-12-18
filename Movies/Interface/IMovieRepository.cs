﻿using Movies.Business;
using Movies.Models;

namespace Movies.Interface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie? GetMovieById(int id);
        IEnumerable<Movie> GetMovieByName(string name);
        IEnumerable<Movie> GetRecentUpdateMovies(int featureId);
        IEnumerable<Movie> GetMovieByCategory(int categoryId);
        Task<ResponseDTO> CreateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> UpdateMovie(MovieDetail movieDetail);
        Task<ResponseDTO> DeleteMovie(int id);
    }
}