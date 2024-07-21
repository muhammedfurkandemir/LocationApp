using LocationApp.Business.Contracts;
using LocationApp.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Windows.Input;
using System.Xml.Linq;

namespace LocationApp.Business.Old
{
    public class CoordinatePostgresqlManager
    //: ICoordinateService
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
            command.Parameters.AddWithValue("@name", coordinate.name);
            command.Parameters.AddWithValue("@x", coordinate.coordinate_x);
            command.Parameters.AddWithValue("@y", coordinate.coordinate_y);
            command.ExecuteNonQuery();
            var id = (int)command.ExecuteScalar();
            coordinate.id = id;
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
                result.id = (int)reader["id"];
                result.name = reader["name"] as string;
                result.coordinate_x = (double)reader["coordinate_x"];
                result.coordinate_y = (double)reader["coordinate_y"];
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
                    id = (int)reader["id"],
                    name = reader["name"] as string,
                    coordinate_x = (double)reader["coordinate_x"],
                    coordinate_y = (double)reader["coordinate_y"]
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
            command.Parameters.AddWithValue("@name", coordinate.name);
            command.Parameters.AddWithValue("@x", coordinate.coordinate_x);
            command.Parameters.AddWithValue("@y", coordinate.coordinate_y);
            command.ExecuteNonQuery();
            _npgsqlConnection.Close();

            return coordinate;
        }
    }
}
