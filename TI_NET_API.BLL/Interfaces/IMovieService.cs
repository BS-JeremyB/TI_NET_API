using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.BLL.Interfaces
{
    public interface IMovieService : IBaseService
    {
        public IEnumerable<Movie> GetAll();
        public Movie? GetById(int id);
        public Movie? Update(int id, Movie movie);
        public Movie? Patch(int id, Movie movie);
        public bool Delete(int id);
        public Movie? Create(Movie movie);
    }
}
