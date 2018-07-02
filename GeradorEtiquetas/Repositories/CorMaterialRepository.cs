using GeradorEtiquetas.Classes;
using GeradorEtiquetas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorEtiquetas.Repositories
{
    public class CorMaterialRepository
    {
        public IEnumerable<MaterialCor> Get()
        {
            var lstCorMaterial = new List<MaterialCor>();

            using (var db = new DatabaseConnect())
                using (var reader = db.Query("SELECT Id, Nome FROM MaterialCor;"))
                    while (reader.Read())
                        lstCorMaterial.Add(new MaterialCor
                        {
                            Id = (int)reader["Id"],
                            Nome = (string)reader["Nome"]
                        });
            return lstCorMaterial;
        }

        public MaterialCor Criar(string nome)
        {
            var query = $"INSERT INTO MaterialCor (Nome) VALUES ('{nome}') RETURNING id;";

            using (var db = new DatabaseConnect())
            {
                using (var reader = db.Query(query))
                    if (reader.Read())
                        return new MaterialCor
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Nome = nome
                        };
                return null;
            }
        }
    }
}
