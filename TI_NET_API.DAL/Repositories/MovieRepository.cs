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
        public Movie? Create(Movie movie)
        {
            FakeDB.Movies.Add(movie);
            return movie;
        }

        public bool Delete(Movie movie)
        {
            return FakeDB.Movies.Remove(movie);

        }

        public IEnumerable<Movie> GetAll()
        {
            return FakeDB.Movies;
        }

        public Movie? GetById(int id)
        {
            return FakeDB.Movies.SingleOrDefault(x => x.Id == id);
        }

        public Movie? Patch(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Movie? Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
