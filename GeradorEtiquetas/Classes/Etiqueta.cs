using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorEtiquetas.Classes
{
    public class Etiqueta
    {
        public Etiqueta(string tipo, Referencia referencia, MaterialCor materialCor)
        {
            Numeracoes = new List<Numeracao>();
            Tipo = tipo;
            Referencia = referencia;
            MaterialCor = materialCor;
        }

        public string Tipo { get; set; }
        public Referencia Referencia { get; set; }
        public MaterialCor MaterialCor { get; set; }
        public IList<Numeracao> Numeracoes { get; set; }

        public void AdicionarNumeracao(int numero, int quantidade)
        {
            Numeracoes.Add(new Numeracao
            {
                Numero = numero,
                Quantidade = quantidade
            });
        }

        public string MontarCodigoDeBarras(int numeracao)
        {
            var referencia = Referencia.Id.ToString().PadLeft(4, '0');
            var material = MaterialCor.Id.ToString().PadLeft(4, '0');
            var numero = numeracao.ToString().PadLeft(2, '0');

            return referencia + material + numero + "09";

            //0000 | 0000 | 00
            // ref  | mat   | nº
        }
    }
}
