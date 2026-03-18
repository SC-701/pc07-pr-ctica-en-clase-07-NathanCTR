using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad Nombre es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}", ErrorMessage = "El formato deL Nombre debe ser ###/ABC")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad Descripcion es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}", ErrorMessage = "El formato de la Descripcion debe ser ###/ABC")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "La propiedad Precio es requerida")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad Stock es requerida")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La propiedad CodigoBarras es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}", ErrorMessage = "El formato del CodigoBarras debe ser ###/ABC")]
        public string? CodigoBarras { get; set; }

        public decimal PrecioCRC { get; set; }
        public decimal PrecioUSD { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }
    public class ProductoDetalle : ProductoResponse
    {
        public bool RevisionValida { get; set; }
        public bool RegistroValido { get; set; }
    }
}
