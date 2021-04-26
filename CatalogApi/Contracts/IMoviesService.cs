using CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Contracts
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int movieId);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> DeleteMovieItem(int movieId);
        Task<Movie> UpdateMovie(Movie movie);
    }


    public interface IShimonService
    {
        void Test();
    }

    public class ShimonService : IShimonService
    {
        public void Test()
        {
            Console.WriteLine("WOW");
        }
    }
}
