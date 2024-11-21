using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.BLL.Base;
using TI_NET_API.BLL.Exceptions;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Movie> GetAll()
        {
            try
            {
                _repository.SetPaginationParams(PaginationParams);
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération de la liste : {ex.Message}");
            }
        }

        public Movie? GetById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la récupération du film : {ex.Message}");
            }
        }

        public Movie? Create(Movie movie)
        {
            try
            {
                if (movie is null)
                {
                    throw new ArgumentNullException(nameof(movie), "Le film ne peut pas être null");
                }

                if (string.IsNullOrWhiteSpace(movie.Title))
                {
                    throw new ArgumentException("Le titre du film est obligatoire");
                }

                return _repository.Create(movie);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la création du film : {ex.Message}");
            }
        }

        public Movie? Update(int id, Movie movie)
        {
            try
            {
                Movie? movieToUpdate = _repository.GetById(id);
                if (movieToUpdate is null)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(movie.Title))
                {
                    throw new ArgumentException("Le titre du film est obligatoire");
                }


                movieToUpdate.Title = movie.Title;
                movieToUpdate.Synopsis = movie.Synopsis;
                movieToUpdate.Director = movie.Director;
                movieToUpdate.Release = movie.Release;
                return _repository.Update(movieToUpdate);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la mise à jour du film : {ex.Message}");
            }
        }

        public Movie? Patch(int id, Movie movie)
        {
            try
            {
                Movie? movieToPatch = _repository.GetById(id);
                if (movieToPatch is null)
                {
                    return null;
                }
                movieToPatch.Release = movie.Release;
                return _repository.Patch(movieToPatch);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la mise à jour partielle du film : {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Movie? movie = _repository.GetById(id);
                if (movie is null)
                {
                    return false;
                }
                return _repository.Delete(movie);
            }
            catch (Exception ex)
            {
                throw new CustomSqlException($"Erreur lors de la suppression du film : {ex.Message}");
            }
        }
    }
}