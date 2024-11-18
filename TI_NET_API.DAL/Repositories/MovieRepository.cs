using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DAL.Context;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly FakeDB _db;

        public MovieRepository(FakeDB db)
        {
            _db = db;
        }

        public Movie? Create(Movie movie)
        {
            if(movie is not null)
            {
                movie.Id = ++_db.IdCount;

                _db.Movies.Add(movie);
            }
            return movie;
        }

        public bool Delete(Movie movie)
        {
            return _db.Movies.Remove(movie);

        }

        public IEnumerable<Movie> GetAll()
        {
            return _db.Movies;
        }

        public Movie? GetById(int id)
        {
            return _db.Movies.SingleOrDefault(x => x.Id == id);
        }

        public Movie? Patch(Movie movie)
        {
            return movie is not null ? movie : null;
        }

        public Movie? Update(Movie movie)
        {
            return movie is not null ?  movie : null;

        }
    }
}
