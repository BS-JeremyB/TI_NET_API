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

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@Release", movie.Release);

                _connection.Open();
                movie.Id = (int)command.ExecuteScalar();
                _connection.Close();

                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Delete(Movie movie)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.[MOVIE] WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", movie.Id);

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public IEnumerable<Movie> GetAll()
        {

            List<Movie> movies = new List<Movie>();
            try
            {
                using SqlCommand command = _connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.[MOVIE]";
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
                throw new Exception(ex.Message, ex);
            }
        }
        public Movie? GetById(int id)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.[MOVIE] WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Movie movie = new Movie
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Synopsis = (string)reader["Synopsis"],
                        Director = (string)reader["Director"],
                        Release = (DateTime)reader["Release"]
                    };
                    _connection.Close();
                    return movie;
                }
                _connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Movie? Update(Movie movie)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "UPDATE dbo.[MOVIE] SET Title = @Title, Synopsis = @Synopsis, " +
                                    "Director = @Director, Release = @Release " +
                                    "WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", movie.Id);
                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@Release", movie.Release);

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0 ? movie : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Movie? Patch(Movie movie)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "UPDATE dbo.[MOVIE] SET Release = @Release WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", movie.Id);
                command.Parameters.AddWithValue("@Release", movie.Release);

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0 ? movie : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
