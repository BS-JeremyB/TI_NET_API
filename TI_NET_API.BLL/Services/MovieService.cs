using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Movie? Create(Movie movie)
        {
            return _repository.Create(movie);
        }

        public bool Delete(int id)
        {
            Movie? movie = _repository.GetById(id);
            if (movie is not null)
            {
                return _repository.Delete(movie);
                
            }

            return false;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _repository.GetAll();
        }

        public Movie? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Movie? Patch(int id, Movie movie)
        {
            Movie? movieToPatch = _repository.GetById(id);
            if (movieToPatch is not null)
            {
                movieToPatch.Release = movie.Release;

                return _repository.Patch(movieToPatch);
            }

            return null;
        }

        public Movie? Update(int id, Movie movie)
        {
            Movie? movieToUpdate = _repository.GetById(id);
            if (movieToUpdate is not null)
            {
                movieToUpdate.Title = movie.Title;
                movieToUpdate.Synopsis = movie.Synopsis;
                movieToUpdate.Release = movie.Release;
                movieToUpdate.Director = movie.Director;

                return _repository.Update(movieToUpdate);
            }

            return null;

        }
    }
}
