using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguracion _configuracion;

        public TipoCambioServicio(HttpClient httpClient, IConfiguracion configuracion)
        {
            _httpClient = httpClient;
            _configuracion = configuracion;
        }
        public async Task<decimal> ObtenerTipoCambio()
        {
            var urlBase = _configuracion.ObtenerValor("BancoCentralCR:UrlBase");
            var token = _configuracion.ObtenerValor("BancoCentralCR:BearerToken");

            var fecha = DateTime.Now.ToString("yyyy/MM/dd");

            var url = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contenido = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<TipoCambioResponse>(contenido);

            return data.datos[0].indicadores[0].series[0].valorDatoPorPeriodo;

        }
    }
}
