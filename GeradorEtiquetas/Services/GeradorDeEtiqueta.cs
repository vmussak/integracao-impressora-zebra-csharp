using GeradorEtiquetas.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorEtiquetas.Services
{
    public class GeradorDeEtiqueta
    {
        public void Gerar(Etiqueta etiqueta)
        {
            StringBuilder sb = new StringBuilder();

            foreach(var numeracao in etiqueta.Numeracoes)
            {
                if (numeracao.Quantidade == 0) continue;

                sb.AppendLine();
                sb.AppendLine("N");
                sb.AppendLine("q454");
                sb.AppendLine("Q296,26");
                sb.AppendLine($"A26,20,0,4,1,1,N,\"REF:{etiqueta.Referencia.Nome}\"");
                sb.AppendLine($"A387,20,0,5,1,1,N,\"{numeracao.Numero}\"");
                sb.AppendLine($"A25,76,0,4,1,1,N,\"{etiqueta.MaterialCor.Nome}\"");
                sb.AppendLine($"B130,115,0,UA0,2,2,152,B,\"{etiqueta.MontarCodigoDeBarras(numeracao.Numero)}\"");
                sb.AppendLine($"P{numeracao.Quantidade},1");
            }

            RawPrinterHelper.SendStringToPrinter(ConfigurationManager.AppSettings["printerName"], sb.ToString());
        }
    }
}
