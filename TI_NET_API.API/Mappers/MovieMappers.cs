using TI_NET_API.DAL.Context;
using TI_NET_API.API.DTO;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Mappers
{
    public static class MovieMappers
    {
        public static Movie ToMovie(this MovieCreateFormDTO movieDTO)
        {
            return new Movie
            {
                Id = ++FakeDB.IdCount,
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Synopsis = movieDTO.Synopsis,
                Release = movieDTO.Release,
            };
        }
    }
}
