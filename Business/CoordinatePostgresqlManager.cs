using LocationApp.Business.Contracts;
using LocationApp.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
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
            _npgsqlConnection.Open();
        }

        public Coordinate Add(Coordinate coordinate)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            var id = new Random().Next();
            command.CommandText = "INSERT INTO coordinates (id, name, coordinate_x, coordinate_y) " +
                "VALUES (@id, @name, @x, @y)";

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", coordinate.Name);
            command.Parameters.AddWithValue("@x", coordinate.X);
            command.Parameters.AddWithValue("@y", coordinate.Y);
            command.ExecuteNonQuery();
            return coordinate;
        }

        public bool Delete(int id)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            command.CommandText = $"DELETE FROM coordinates WHERE id = {id}";
            command.ExecuteNonQuery();
            return true;
        }

        public Coordinate Get(int id)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            command.CommandText = $"SELECT * FROM coordinates WHERE id = {id}";
            using NpgsqlDataReader reader = command.ExecuteReader();
            var result = new Coordinate();
            if (reader.Read())
            {
                result.Id = (int)reader["id"];
                result.Name = reader["name"] as string;
                result.X = (double)reader["coordinate_x"];
                result.Y = (double)reader["coordinate_y"];
            }
            return result;
        }

        public List<Coordinate> GetAll()
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
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
            return result;
        }

        public Coordinate Update(int id, Coordinate coordinate)
        {
            using var command = new NpgsqlCommand();
            command.Connection = _npgsqlConnection;
            command.CommandText = $"UPDATE coordinates SET name = '{coordinate.Name}', coordinate_x = {coordinate.X}, coordinate_y = {coordinate.Y} WHERE id = {id}";
            command.ExecuteNonQuery();
            return coordinate;
        }
    }
}
