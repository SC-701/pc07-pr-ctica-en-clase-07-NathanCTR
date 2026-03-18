using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Abstracciones.Modelos
{
    public class TipoCambioResponse
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public List<Dato> datos  { get; set; }
    }
    public class Dato
    {
        public string titulo { get; set; }
        public List<Indicador> indicadores { get; set; }
    }
    public class Indicador
    {
        public string codigoIndicador { get; set; }
        public List<Serie> series  { get; set; }
    }
    public class Serie
    {
        public DateTime fecha { get; set; }
        public decimal valorDatoPorPeriodo { get; set; }
    }
}
