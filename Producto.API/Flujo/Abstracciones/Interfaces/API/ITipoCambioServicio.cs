using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ITipoCambioServicio
    {
        Task<decimal> ObtenerTipoCambio();
    }
}
