using System.Windows.Forms;
using IServicio.ListaPrecio;
using IServicio.ListaPrecio.DTOs;
using PresentacionBase.Formularios;
using StructureMap;

namespace Presentacion.Core.Articulo
{
    public partial class _00033_Abm_ListaPrecio : FormAbm
    {
        private readonly IListaPrecioServicio _listaPrecio;

        public _00033_Abm_ListaPrecio(TipoOperacion tipoOperacion, long? entidadId = null, IListaPrecioServicio listaPrecio = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _listaPrecio = ObjectFactory.GetInstance<IListaPrecioServicio>();
            
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                var resultados = (ListaPrecioDto)_listaPrecio.Obtener(entidadId.Value);

                if (resultados == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }

                txtDescripcion.Text = resultados.Descripcion;

                nudPorcentaje.Value = resultados.PorcentajeGanancia;

                chkPedirAutorizacion.Checked = resultados.NecesitaAutorizacion;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else 
            {
                btnEjecutar.Text = "Nuevo";
            }
        }

        public override bool VerificarDatosObligatorios()
        {
            return !string.IsNullOrEmpty(txtDescripcion.Text);
        }

        public override bool VerificarSiExiste(long? id = null)
        {
            return _listaPrecio.VerificarSiExiste(txtDescripcion.Text, id);
        }

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new ListaPrecioDto();


            nuevoRegistro.Descripcion = txtDescripcion.Text;
            nuevoRegistro.PorcentajeGanancia = nudPorcentaje.Value;
            nuevoRegistro.NecesitaAutorizacion = chkPedirAutorizacion.Checked;
            nuevoRegistro.Eliminado = false;

            _listaPrecio.Insertar(nuevoRegistro);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarRegistro = new ListaPrecioDto();
            modificarRegistro.Id = EntidadId.Value;
            modificarRegistro.Descripcion = txtDescripcion.Text;
            modificarRegistro.PorcentajeGanancia = nudPorcentaje.Value;
            modificarRegistro.NecesitaAutorizacion = chkPedirAutorizacion.Checked;
            modificarRegistro.Eliminado = false;

            _listaPrecio.Modificar(modificarRegistro);
        }


        public override void EjecutarComandoEliminar()
        {
            _listaPrecio.Eliminar(EntidadId.Value);
        }

        public override void LimpiarControles(Form formulario)
        {
            base.LimpiarControles(formulario);

            txtDescripcion.Focus();
        }
    }
}
