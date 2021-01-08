using IServicio.Departamento;
using IServicio.Localidad;
using IServicio.Persona;
using IServicio.Persona.DTOs;
using IServicio.Provincia;
using Presentacion.Core.CondicionIva;
using Presentacion.Core.Departamento;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using PresentacionBase.Formularios;
using StructureMap;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Cliente
{
    public partial class _00010_Abm_Cliente : FormAbm
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly IDepartamentoServicio _departamentoServicio;
        private readonly ILocalidadServicio _localidadServicio;
        private readonly ICondicionIvaServicio _condicionIvaServicio;

        public _00010_Abm_Cliente(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _clienteServicio = ObjectFactory.GetInstance<IClienteServicio>();
             _provinciaServicio = ObjectFactory.GetInstance<IProvinciaServicio>();
            _departamentoServicio = ObjectFactory.GetInstance<IDepartamentoServicio>();
            _localidadServicio = ObjectFactory.GetInstance<ILocalidadServicio>();
            _condicionIvaServicio = ObjectFactory.GetInstance<ICondicionIvaServicio>();

        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if(entidadId.HasValue)
            {
                var resultado = (ClienteDto)_clienteServicio.Obtener(typeof(ClienteDto), entidadId.Value);

                if(resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");

                    Close();
                }

                // =============== Datos Cliente ========== //

                txtApellido.Text = resultado.Apellido;

                txtNombre.Text = resultado.Nombre;

                txtDni.Text = resultado.Dni;

                txtTelefono.Text = resultado.Telefono;

                txtDomicilio.Text = resultado.Direccion;

                cmbProvincia.SelectedValue = resultado.ProvinciaId;
                cmbDepartamento.SelectedValue = resultado.DepartamentoId;
                cmbLocalidad.SelectedValue = resultado.LocalidadId;
                txtMail.Text = resultado.Mail;
                cmbCondicionIva.SelectedValue = resultado.CondicionIva;

                // =============== Datos ========== //

                chkActivarCuentaCorriente.Checked = resultado.ActivarCtaCte;

                chkLimiteCompra.Checked = resultado.TieneLimiteCompra;

                nudLimiteCompra.Value = resultado.MontoMaximoCtaCte;

                PoblarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbProvincia.SelectedValue = resultado.ProvinciaId;

                

                PoblarComboBox(cmbDepartamento, _departamentoServicio.ObtenerPorProvincia(resultado.ProvinciaId), "Descripcion", "Id");

                cmbDepartamento.SelectedValue = resultado.DepartamentoId;

                PoblarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorDepartamento(resultado.DepartamentoId), "Descripcion", "Id");

                cmbLocalidad.SelectedValue = resultado.LocalidadId;

                PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbCondicionIva.SelectedValue = resultado.CondicionIvaId;


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


                PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");

                btnEjecutar.Text = "Grabar";

                LimpiarControles(this, false);




            }


        }


        public override bool VerificarDatosObligatorios()
        {
            
            if (string.IsNullOrEmpty(txtApellido.Text)) return false;
            if (string.IsNullOrEmpty(txtNombre.Text)) return false;
            if (string.IsNullOrEmpty(txtDni.Text)) return false;
            if (string.IsNullOrEmpty(txtTelefono.Text)) return false;
            if (string.IsNullOrEmpty(txtDomicilio.Text)) return false;
            if (cmbProvincia.Items.Count <= 0) return false;
            if (cmbDepartamento.Items.Count <= 0) return false;
            if (cmbLocalidad.Items.Count <= 0) return false;
            if (string.IsNullOrEmpty(txtMail.Text)) return false;
            if (cmbCondicionIva.Items.Count <= 0) return false;
            return true;
        }


        public override void EjecutarComandoNuevo()
        {
            var nuevoCliente = new ClienteDto
            {

                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                CondicionIvaId = (long)cmbCondicionIva.SelectedValue,

                //------------------------------------------------//


                ActivarCtaCte = chkActivarCuentaCorriente.Checked,
                TieneLimiteCompra =chkLimiteCompra.Checked,
                MontoMaximoCtaCte = nudLimiteCompra.Value,
                Eliminado = false

            };

            _clienteServicio.Insertar(nuevoCliente);
        }


        public override void EjecutarComandoModificar()
        {
            var modificarCliente = new ClienteDto
            {
                Id = EntidadId.Value,
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                ProvinciaId = (long)cmbProvincia.SelectedValue,
                DepartamentoId = (long)cmbDepartamento.SelectedValue,
                LocalidadId = (long)cmbLocalidad.SelectedValue,
                Mail = txtMail.Text,
                CondicionIvaId = (long)cmbCondicionIva.SelectedValue,


                ActivarCtaCte = chkActivarCuentaCorriente.Checked,
                TieneLimiteCompra = chkLimiteCompra.Checked,
                MontoMaximoCtaCte = nudLimiteCompra.Value,

                Eliminado = false

            };

            _clienteServicio.Modificar(modificarCliente);
        }

        public override void EjecutarComandoEliminar()
        {
            _clienteServicio.Eliminar(typeof(ClienteDto), EntidadId.Value);

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
        private void btnNuevaCondicionIva_Click(object sender, System.EventArgs e)
        {
            var form = new _00014_Abm_CondicionIva(TipoOperacion.Nuevo);
            form.ShowDialog();

            if(form.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }

        protected override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {
            base.LimpiarControles(obj, tieneValorAsociado);

        }

























        /*
       Universidad Tecnologica Nacional
       Facultad Regional Tucuman
       Tecnicatura Universitaria en Programacion

       Programacion I 2020

       RIVAS, FLORENCIA ANABELA 
       DNI 36040430

       Comision 4
       */






    }











}

