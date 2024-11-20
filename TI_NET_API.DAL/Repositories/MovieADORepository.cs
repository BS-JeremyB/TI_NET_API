using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Repositories
{
    public class MovieADORepository : IMovieRepository
    {
        private readonly SqlConnection _connection;

        public MovieADORepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Movie? Create(Movie movie)
        {
            try 
            {

                using SqlCommand command = _connection.CreateCommand();

                command.CommandText = "INSERT INTO dbo.[MOVIE] (Title, Synopsis, Director, Release) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Title, @Synopsis, @Director, @Release)";
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@Release", movie.Release);

                _connection.Open();
                movie.Id = (int)command.ExecuteScalar();
                _connection.Close();

                return movie;
            }
            catch(Exception ex) 
            {
                throw new Exception($"Erreur lors de l'insertion d'un film : {ex.Message}", ex);
            }
        }

        public bool Delete(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            
            List<Movie> movies = new List<Movie>();

            try
            {

                using SqlCommand command = _connection.CreateCommand();
                
                command.CommandText = "SELECT * FROM dbo.[MOVIE]";
                command.CommandType = CommandType.Text;

                _connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                    
                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Synopsis = (string)reader["Synopsis"],
                        Director = (string)reader["Director"],
                        Release = (DateTime)reader["Release"]
                    });
                }
                    
                _connection.Close();

                return movies;

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de la liste", ex);
            }
        }

        public Movie? GetById(int id)
        {
            throw new NotImplementedException();
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
