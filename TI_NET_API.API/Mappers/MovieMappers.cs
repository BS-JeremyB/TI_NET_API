
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
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Synopsis = movieDTO.Synopsis,
                Release = movieDTO.Release,
            };
        }

        public static Movie ToMovie(this MovieUpdateFormDTO movieDTO)
        {
            return new Movie
            {
                Title = movieDTO.Title,
                Director = movieDTO.Director,
                Synopsis = movieDTO.Synopsis,
                Release = movieDTO.Release,
            };
        }

        public static Movie ToMovie(this MoviePatchFormDTO movieDTO)
        {
            return new Movie
            {
                Release = movieDTO.Release,
            };
        }
    
    
        public static MovieViewDTO ToDTO(this Movie movie)
        {
            return new MovieViewDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Synopsis = movie.Synopsis,
                Release = movie.Release,
            };
        }

        public static MovieListViewDTO ToListDTO(this Movie movie)
        {
            return new MovieListViewDTO
            {
                Id = movie.Id,
                Title = movie.Title,
            };
        }
    }
}
