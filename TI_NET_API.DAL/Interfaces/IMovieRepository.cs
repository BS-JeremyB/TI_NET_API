using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Interfaces
{
    public interface IMovieRepository : IBaseRepository
    {
        public IEnumerable<Movie> GetAll();
        public Movie? GetById(int id);
        public Movie? Update(Movie movie);
        public Movie? Patch(Movie movie);
        public bool Delete(Movie movie); 
        public Movie? Create(Movie movie);



    }
}
