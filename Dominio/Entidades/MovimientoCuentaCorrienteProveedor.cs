using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aplicacion.Constantes;
using Dominio.MetaData;

namespace Dominio.Entidades
{
    [Table("Movimiento_CuentaCorrienteProveedor")]
    [MetadataType(typeof(IMovimientoCuentaCorrienteProveedor))]
    public class MovimientoCuentaCorrienteProveedor : Movimiento
    {

       // public long UsuarioId { get; set; }

        public long ProveedorId { get; set; }


        // Propiedades de Navegacion
        public virtual Proveedor Proveedor { get; set; }
     //   public virtual Usuario Usuario { get; set; }


    }
}
