using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorEtiquetas.Repositories
{
    public class DatabaseConnect : IDisposable
    {
        public DatabaseConnect()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["geradorEtiquetas"].ConnectionString;
            _connection = new NpgsqlConnection(connectionString);
        }
        
        private readonly NpgsqlConnection _connection;

        public NpgsqlDataReader Query(string query)
        {
            _connection.Open();

            return new NpgsqlCommand(query, _connection).ExecuteReader();
        }

        public int Command(string query)
        {
            _connection.Open();

            return new NpgsqlCommand(query, _connection).ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
