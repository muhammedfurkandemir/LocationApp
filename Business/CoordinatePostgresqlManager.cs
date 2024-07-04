using LocationApp.Business.Contracts;
using LocationApp.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Windows.Input;
using System.Xml.Linq;

namespace LocationApp.Business
{
    public class CoordinatePostgresqlManager : ICoordinateService
    {

        private readonly NpgsqlConnection _npgsqlConnection;

        public CoordinatePostgresqlManager(IConfiguration configuration)
        {
            _npgsqlConnection = new NpgsqlConnection(
                connectionString: configuration.GetConnectionString("PostgresConnection"));            
        }

        public Coordinate Add(Coordinate coordinate)
        {
            if (coordinate is null)
            {
                return null;
            }

            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            _npgsqlConnection.Open();
            command.CommandText = "INSERT INTO coordinates ( name, coordinate_x, coordinate_y) " +
                "VALUES (@name, @x, @y) RETURNING id";
            command.Parameters.AddWithValue("@name", coordinate.Name);
            command.Parameters.AddWithValue("@x", coordinate.X);
            command.Parameters.AddWithValue("@y", coordinate.Y);
            command.ExecuteNonQuery();
            var id = (int)command.ExecuteScalar();
            coordinate.Id = id;
            _npgsqlConnection.Close();
            return coordinate;
        }

        public bool Delete(int id)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            _npgsqlConnection.Open();
            command.CommandText = $"DELETE FROM coordinates WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _npgsqlConnection.Close();
            return true;
        }

        public Coordinate Get(int id)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            _npgsqlConnection.Open();
            command.CommandText = $"SELECT * FROM coordinates WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            using NpgsqlDataReader reader = command.ExecuteReader();           
            var result = new Coordinate();
            if (reader.Read())
            {
                result.Id = (int)reader["id"];
                result.Name = reader["name"] as string;
                result.X = (double)reader["coordinate_x"];
                result.Y = (double)reader["coordinate_y"];
            }
            _npgsqlConnection.Close();
            return result;
        }

        public List<Coordinate> GetAll()
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            _npgsqlConnection.Open();

            command.CommandText = "SELECT * FROM coordinates";
            using NpgsqlDataReader reader = command.ExecuteReader();
            var result = new List<Coordinate>();
            while (reader.Read())
            {
                result.Add(new Coordinate
                {
                    Id = (int)reader["id"],
                    Name = reader["name"] as string,
                    X = (double)reader["coordinate_x"],
                    Y = (double)reader["coordinate_y"]
                });
            }
            _npgsqlConnection.Close();
            return result;
        }

        public Coordinate Update(int id, Coordinate coordinate)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            _npgsqlConnection.Open();

            command.CommandText = $"UPDATE coordinates " +
                $"SET name = '@name', coordinate_x = @x, coordinate_y = @y WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", coordinate.Name);
            command.Parameters.AddWithValue("@x", coordinate.X);
            command.Parameters.AddWithValue("@y", coordinate.Y);
            command.ExecuteNonQuery();
            _npgsqlConnection.Close();

            return coordinate;
        }
    }
}
