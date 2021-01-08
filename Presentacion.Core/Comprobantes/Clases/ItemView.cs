using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Comprobantes.Clases
{
    public class ItemView
    {

        public long Id { get; set; }

        public long ArticuloId { get; set; }


        public string CodigoBarra { get; set; }


        public string Descripcion { get; set; }


        public decimal Cantidad { get; set; }

        public decimal Iva { get; set; }

        public decimal Precio { get; set; }
        public string PrecioStr => Precio.ToString("C", new CultureInfo("es-Ar"));

        public decimal SubTotal => Cantidad * Precio;
        public string SubTotalStr => SubTotal.ToString("C", new CultureInfo("es-Ar"));

        public bool EsArticuloAlternativo { get; set; } 

        public long ListaPrecioId { get; set; }

        public bool IngresoPorBascula { get; set; } 


    }
}
