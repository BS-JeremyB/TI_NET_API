using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Repositories
{
    public class UserADORepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserADORepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public User? Create(User user)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO dbo.[USER] (Email, FirstName, LastName, Password, Role) 
            OUTPUT INSERTED.Id 
            VALUES (@Email, @FirstName, @LastName, @Password, @Role)";

                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", (object)user.FirstName ?? DBNull.Value);
                command.Parameters.AddWithValue("@LastName", (object)user.LastName ?? DBNull.Value);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", user.Role.ToString());

                _connection.Open();
                user.Id = (int)command.ExecuteScalar();
                _connection.Close();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Delete(User user)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.[USER] WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", user.Id);

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

        public IEnumerable<User> GetAll()
        {
            try
            {
                List<User> users = new List<User>();
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.[USER]";

                _connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Email = (string)reader["Email"],
                        FirstName = reader["FirstName"] as string,
                        LastName = reader["LastName"] as string,
                        Password = (string)reader["Password"],
                        Role = (Role)(byte)reader["Role"]
                    });
                }

                _connection.Close();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public User? GetById(int id)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.[USER] WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Email = (string)reader["Email"],
                        FirstName = reader["FirstName"] as string,
                        LastName = reader["LastName"] as string,
                        Password = (string)reader["Password"],
                        Role = (Role)(byte)reader["Role"]
                    };
                    _connection.Close();
                    return user;
                }
                _connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public User? GetByEmail(string email)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.[USER] WHERE Email = @Email";
                command.Parameters.AddWithValue("@Email", email);

                _connection.Open();
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Email = (string)reader["Email"],
                        FirstName = reader["FirstName"] as string,
                        LastName = reader["LastName"] as string,
                        Password = (string)reader["Password"],
                        Role = (Role)(byte)reader["Role"]
                    };
                    _connection.Close();
                    return user;
                }
                _connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public User? Update(User user)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = @"
            UPDATE dbo.[USER] 
            SET Email = @Email, 
                FirstName = @FirstName, 
                LastName = @LastName, 
                Password = @Password, 
                Role = @Role 
            WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", (object)user.FirstName ?? DBNull.Value);
                command.Parameters.AddWithValue("@LastName", (object)user.LastName ?? DBNull.Value);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", user.Role.ToString());

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0 ? user : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public User? Patch(User user)
        {
            try
            {
                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = @"
            UPDATE dbo.[USER] 
            SET Role = @Role 
            WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Role", user.Role.ToString());

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0 ? user : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
    }
}
