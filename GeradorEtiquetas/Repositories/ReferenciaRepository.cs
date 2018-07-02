using GeradorEtiquetas.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorEtiquetas.Repositories
{
    public class ReferenciaRepository
    {
        public Referencia BuscarPorNome(string nome)
        {
            var query = $"SELECT Id, Nome FROM Referencia WHERE Nome LIKE '{nome}';";

            using (var db = new DatabaseConnect())
            using (var reader = db.Query(query))
                if (reader.Read())
                    return new Referencia
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"]
                    };
            return null;
        }

        public Referencia CriarReferencia(string nome)
        {
            var query = $"INSERT INTO Referencia (Nome) VALUES ('{nome}') RETURNING id;";

            using (var db = new DatabaseConnect())
            {
                using (var reader = db.Query(query))
                    if (reader.Read())
                        return new Referencia
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Nome = nome
                        };
                return null;
            }
        }
    }
}
