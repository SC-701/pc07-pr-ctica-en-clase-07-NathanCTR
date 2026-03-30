using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{

    public class EditarModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;
        public ProductoRequest productoRequest { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; } = default!;
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } = default!;
        [BindProperty]
        public Guid subCategoriaSeleccionado { get; set; } = default!;
        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();
            
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategoriasAsync();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
                if (producto != null)
                {
                    categoriaSeleccionada = Guid.Parse(categorias.Where(m => m.Text == producto.Categoria).FirstOrDefault().Value);                    
                    subCategorias = (await ObtenerSubcategoriasAsync(subCategoriaSeleccionado)).Select(a =>
                        new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Nombre.ToString(),
                            Selected = a.Nombre == producto.SubCategoria
                        }).ToList();
                    subCategoriaSeleccionado = Guid.Parse(subCategorias.Where(m => m.Text == producto.SubCategoria).FirstOrDefault().Value);
                }

            }
            return Page();
        }
        public async Task<ActionResult> OnPost()
        {
            if (producto.Id == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = new HttpClient();
            
            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, producto.Id.ToString()),new ProductoRequest { IdSubCategoria=subCategoriaSeleccionado, Nombre=producto.Nombre, Descripcion= producto.Descripcion, Precio = producto.Precio, Stock = producto.Stock, CodigoBarras = producto.CodigoBarras });
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
        private async Task ObtenerCategoriasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
                categorias = resultadoDeserializado.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Nombre.ToString()
                                  }).ToList();
            }
        }
        public async Task<JsonResult> OnGetObtenerSubcategorias(Guid categoriaId)
        {
            var subCategorias = await ObtenerSubcategoriasAsync(categoriaId);
            return new JsonResult(subCategorias);
        }

        private async Task<List<SubCategoria>> ObtenerSubcategoriasAsync(Guid categoriaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategoria");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }
            return new List<SubCategoria>();
        }
    }
}
