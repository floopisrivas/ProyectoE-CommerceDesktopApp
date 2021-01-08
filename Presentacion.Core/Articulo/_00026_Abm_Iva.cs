using IServicio.Iva;
using IServicio.Iva.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00026_Abm_Iva : FormAbm
    {

        private readonly IIvaServicio _ivaServicio;
        public _00026_Abm_Iva(TipoOperacion tipoOperacion, long? entidadId = null, IIvaServicio ivaServicio = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _ivaServicio = ObjectFactory.GetInstance<IIvaServicio>();
            
        }



        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultado = (IvaDto)_ivaServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultado.Descripcion;

                nudPorcentaje.Value = resultado.Porcentaje;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else 
            {
                btnEjecutar.Text = "Nuevo";
            }
        }




        public override bool VerificarSiExiste(long? id = null)
        {
            return base.VerificarSiExiste(id);
        }



        public override bool VerificarDatosObligatorios()
        {
            return !string.IsNullOrEmpty(txtDescripcion.Text);
        }


        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new IvaDto();
            nuevoRegistro.Descripcion = txtDescripcion.Text;
            nuevoRegistro.Porcentaje = nudPorcentaje.Value;
            nuevoRegistro.Eliminado = false;

            
            _ivaServicio.Insertar(nuevoRegistro);
            
            
            
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new IvaDto();
            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.Porcentaje = nudPorcentaje.Value;
            modificarRegistro.Eliminado = false;

            _ivaServicio.Modificar(modificarRegistro);
        }

        public override void EjecutarComandoEliminar()
        {
            _ivaServicio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(Form formulario)
        {
            base.LimpiarControles(formulario);

            txtDescripcion.Focus();

        }
    }
}
