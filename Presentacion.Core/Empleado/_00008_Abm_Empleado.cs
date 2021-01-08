using Aplicacion.Constantes;
using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicio.Provincia;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class _00008_Abm_Empleado : FormAbm
    {

        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;

        public _00008_Abm_Empleado(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _empleadoServicio = ObjectFactory.GetInstance<IEmpleadoServicio>();
            _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);


            if(entidadId.HasValue)
            {
                var resultados = (EmpleadoDto)_empleadoServicio.Obtener(typeof(EmpleadoDto), entidadId.Value);

                if(resultados == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");

                    Close();
                }

                //************Datos************//

                txtLegajo.Text = resultados.Legajo.ToString();
                txtApellido.Text = resultados.Apellido;
                txtNombre.Text = resultados.Nombre;
                txtDni.Text = resultados.Dni;
                txtTelefono.Text = resultados.Telefono;
                txtDomicilio.Text = resultados.Direccion;
                cmbProvincia.SelectedValue = resultados.ProvinciaId;
                cmbDepartamento.SelectedValue = resultados.DepartamentoId;
                cmbLocalidad.SelectedValue = resultados.LocalidadId;
                txtMail.Text = resultados.Mail;

                imgFoto.Image = Imagen.ConvertirImagen(resultados.Foto);

                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbProvincia.SelectedValue = resultados.ProvinciaId;

                PoblarComboBox(cmbDepartamento, _departamentoServicio.ObtenerPorProvincia(resultados.ProvinciaId), "Descripcion", "Id");

                cmbDepartamento.SelectedValue = resultados.DepartamentoId;

                PoblarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorDepartamento(resultados.DepartamentoId), "Descripcion", "Id");

                cmbLocalidad.SelectedValue = resultados.LocalidadId;

                if (TipoOperacion == TipoOperacion.Eliminar)
                {
                    DesactivarControles(this);
                }

            }
            else
            {
                var provincias = _provinciaServicio.Obtener(string.Empty);

                PoblarComboBox(cmbProvincia,
                    provincias,
                    "Descripcion",
                    "Id");

                if (provincias.Any())
                {
                    var departamentos = _departamentoServicio
                        .ObtenerPorProvincia((long)cmbProvincia.SelectedValue);

                    PoblarComboBox(cmbDepartamento,
                        departamentos,
                        "Descripcion",
                        "Id");

                    if (departamentos.Any())
                    {
                        var localidades =
                            _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue);

                        PoblarComboBox(cmbLocalidad,
                            localidades,
                            "Descripcion",
                            "Id");
                    }
                  
                }

                btnEjecutar.Text = "Grabar";

                LimpiarControles(this, false);

            }

        }


        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtLegajo.Text)) return false;
            if (string.IsNullOrEmpty(txtApellido.Text)) return false;
            if (string.IsNullOrEmpty(txtNombre.Text)) return false;
            if (string.IsNullOrEmpty(txtDni.Text)) return false;
            if (string.IsNullOrEmpty(txtTelefono.Text)) return false;
            if (string.IsNullOrEmpty(txtDomicilio.Text)) return false;
            if (cmbProvincia.Items.Count <= 0) return false;
            if (cmbDepartamento.Items.Count <= 0) return false;
            if (cmbLocalidad.Items.Count <= 0) return false;
            if (string.IsNullOrEmpty(txtMail.Text)) return false;

            return true;
        }


        public override void EjecutarComandoNuevo()
        {
            var nuevoLegajo = new EmpleadoDto
            {

                Legajo = int.Parse(txtLegajo.Text),
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,

                Foto = Imagen.ConvertirImagen(imgFoto.Image),

                Eliminado = false
            };

            _empleadoServicio.Insertar(nuevoLegajo);
        }

        public override void EjecutarComandoModificar()
        {
            var modificarLegajo = new EmpleadoDto
            {
                Id = EntidadId.Value,
                Legajo = int.Parse(txtLegajo.Text),
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                Foto = Imagen.ConvertirImagen(imgFoto.Image),

                Eliminado = false

            };

            _empleadoServicio.Modificar(modificarLegajo);

        }

        public override void EjecutarComandoEliminar()
        {
            _empleadoServicio.Eliminar(typeof(EmpleadoDto), EntidadId.Value);
        }

        protected override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

            txtLegajo.Text = _empleadoServicio.ObtenerSiguienteLegajo().ToString();

            txtApellido.Focus();
        }

        private void btnImagen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
                imgFoto.Image = Image.FromFile(openFile.FileName);
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbProvincia.Items.Count <= 0) return;

            PoblarComboBox(cmbDepartamento,
                _departamentoServicio.ObtenerPorProvincia((long)cmbProvincia.SelectedValue), "Descripcion", "Id");

            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void cmbDepartamento_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbDepartamento.Items.Count <= 0) return;

            PoblarComboBox(cmbLocalidad,
                _localidadServicio.ObtenerPorDepartamento((long)cmbDepartamento.SelectedValue), "Descripcion", "Id");
        }

        private void btnNuevaProvincia_Click(object sender, System.EventArgs e)
        {
            var form = new _00002_Abm_Provincia(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }

        }

        private void btnNuevoDepartamento_Click(object sender, System.EventArgs e)
        {
            var form = new _00004_Abm_Departamento(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbDepartamento, _departamentoServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }

        private void btnNuevaLocalidad_Click(object sender, System.EventArgs e)
        {
            var form = new _00006_AbmLocalidad(TipoOperacion.Nuevo);
            form.ShowDialog();

            if (form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbLocalidad, _localidadServicio.Obtener(string.Empty), "Descripcion", "Id");
            }

        }
    }
}
