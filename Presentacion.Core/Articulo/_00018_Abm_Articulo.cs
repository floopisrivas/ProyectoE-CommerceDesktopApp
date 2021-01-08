using Aplicacion.Constantes;
using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using IServicio.Iva;
using IServicio.Marca;
using IServicio.Rubro;
using IServicio.UnidadMedida;
using IServicios.Articulo.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00018_Abm_Articulo : FormAbm
    {

        private readonly IArticuloServicio _articuloServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IIvaServicio _ivaServicio;

        public _00018_Abm_Articulo(TipoOperacion tipoOperacion, long? entidadId = null, IArticuloServicio articuloServicio = null, 
            IMarcaServicio marcaServicio = null, IRubroServicio rubroServicio = null, IUnidadMedidaServicio unidadMedidaServicio = null,
            IIvaServicio ivaServicio = null)
            : base(tipoOperacion, entidadId)
        {

            InitializeComponent();

            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
            _marcaServicio = ObjectFactory.GetInstance<IMarcaServicio>();
            _rubroServicio = ObjectFactory.GetInstance<IRubroServicio>();
            _unidadMedidaServicio = ObjectFactory.GetInstance<IUnidadMedidaServicio>();
            _ivaServicio = ObjectFactory.GetInstance<IIvaServicio>();
           
        }

        public override void CargarDatos(long? entidadId)
        {
            base.CargarDatos(entidadId);

            if (entidadId.HasValue)
            {
                
                
                nudStock.Enabled = false;
                groupPrecio.Enabled = false;

                var resultado = (ArticuloDto)_articuloServicio.Obtener(entidadId.Value);

                if (resultado == null)
                {
                    MessageBox.Show("Ocurrio un error al obtener el registro seleccionado");
                    Close();
                }


                // =============== Datos del Articulo ========== //

                txtCodigo.Text = resultado.Codigo.ToString();
                txtcodigoBarra.Text = resultado.CodigoBarra;
                txtDescripcion.Text = resultado.Descripcion;
                txtAbreviatura.Text = resultado.Abreviatura;
                txtDetalle.Text = resultado.Abreviatura;
                txtUbicacion.Text = resultado.Ubicacion;
                cmbMarca.SelectedValue = resultado.MarcaId;
                cmbRubro.SelectedValue = resultado.RubroId;
                cmbUnidad.SelectedValue = resultado.UnidadMedidaId;
                cmbIva.SelectedValue = resultado.IvaId;


                // =============== Datos del Generales ========== //

                nudStockMin.Value = resultado.StockMinimo;
                ckbDescontarStock.Checked = resultado.DescuentaStock;
                chkPermitirStockNeg.Checked = resultado.PermiteStockNegativo;
                chkActivarLimite.Checked = resultado.ActivarHoraVenta;
                nudLimiteVenta.Value = resultado.LimiteVenta;
                chkActivarHoraVenta.Checked = resultado.ActivarHoraVenta;
                dtpHoraVenta.Value = resultado.HoraLimiteVentaDesde;
                dtpHoraLimiteHasta.Value = resultado.HoraLimiteVentaHasta;


                // =============== Foto del Articulo ========== //


                imgFoto.Image = Imagen.ConvertirImagen(resultado.Foto);


                // =============== ========= //


                PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbMarca.SelectedValue = resultado.MarcaId;


                PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbRubro.SelectedValue = resultado.RubroId;

                PoblarComboBox(cmbUnidad, _unidadMedidaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbUnidad.SelectedValue = resultado.UnidadMedidaId;


                PoblarComboBox(cmbIva, _ivaServicio.Obtener(string.Empty), "Descripcion", "Id");

                cmbIva.SelectedValue = resultado.IvaId;


                txtDescripcion.Text = resultado.Descripcion;

                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);
            }
            else 
            {

                PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");

                PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");

                PoblarComboBox(cmbUnidad, _unidadMedidaServicio.Obtener(string.Empty), "Descripcion", "Id");

                PoblarComboBox(cmbIva, _ivaServicio.Obtener(string.Empty), "Descripcion", "Id");


                btnEjecutar.Text = "Grabar";


                LimpiarControles(this,false);

               
            }



        }


        public override bool VerificarDatosObligatorios()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text)) return false;
            if (string.IsNullOrEmpty(txtcodigoBarra.Text)) return false;
            if (string.IsNullOrEmpty(txtDescripcion.Text)) return false;
            if (cmbMarca.Items.Count <= 0) return false;
            if (cmbRubro.Items.Count <= 0) return false;
            if (cmbUnidad.Items.Count <= 0) return false;
            if (cmbIva.Items.Count <= 0) return false;
            return true;
        }
      

        public override void EjecutarComandoNuevo()
        {
            var nuevoRegistro = new ArticuloCrudDto
            {
                Codigo = int.Parse(txtCodigo.Text),
                CodigoBarra = txtcodigoBarra.Text,
                Descripcion = txtDescripcion.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Ubicacion = txtUbicacion.Text,
                MarcaId = (long)cmbMarca.SelectedValue,
                RubroId = (long)cmbRubro.SelectedValue,
                UnidadMedidaId = (long)cmbUnidad.SelectedValue,
                IvaId = (long)cmbIva.SelectedValue,
                PrecioCosto = nudPrecioCosto.Value,


                //------------------------------------------------//

                StockActual = nudStock.Value,
                StockMinimo = nudStockMin.Value,
                DescuentaStock = ckbDescontarStock.Checked,
                ActivarHoraVenta = chkActivarHoraVenta.Checked,
                LimiteVenta = nudLimiteVenta.Value,
                PermiteStockNegativo = chkPermitirStockNeg.Checked,
                ActivarLimiteVenta = chkActivarLimite.Checked,
                HoraLimiteVentaDesde = dtpHoraVenta.Value,
                HoraLimiteVentaHasta = dtpHoraLimiteHasta.Value,
                

                //------------------------------------------------//

                Foto = Imagen.ConvertirImagen(imgFoto.Image),

                Eliminado = false
            };

            _articuloServicio.Insertar(nuevoRegistro);
        }


        public override void EjecutarComandoModificar()
        {

            var modificarRegistro = new ArticuloCrudDto
            {

                Id = EntidadId.Value,
                Codigo = int.Parse(txtCodigo.Text),
                CodigoBarra = txtcodigoBarra.Text,
                Descripcion = txtDescripcion.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Ubicacion = txtUbicacion.Text,
                MarcaId = (long)cmbMarca.SelectedValue,
                RubroId = (long)cmbRubro.SelectedValue,
                UnidadMedidaId = (long)cmbUnidad.SelectedValue,
                IvaId = (long)cmbIva.SelectedValue,
                PrecioCosto = nudPrecioCosto.Value,


                //------------------------------------------------//

                StockActual = nudStock.Value,
                StockMinimo = nudStockMin.Value,
                DescuentaStock = ckbDescontarStock.Checked,
                ActivarHoraVenta = chkActivarHoraVenta.Checked,
                LimiteVenta = nudLimiteVenta.Value,
                PermiteStockNegativo = chkPermitirStockNeg.Checked,
                ActivarLimiteVenta = chkActivarLimite.Checked,
                HoraLimiteVentaDesde = dtpHoraVenta.Value,
                HoraLimiteVentaHasta = dtpHoraLimiteHasta.Value,


                //------------------------------------------------//

                Foto = Imagen.ConvertirImagen(imgFoto.Image),

                Eliminado = false


            };


            _articuloServicio.Modificar(modificarRegistro);

        }

        protected override void LimpiarControles(object obj, bool tieneValorAsociado = false)
        {

            base.LimpiarControles(obj, tieneValorAsociado);

            txtCodigo.Text = _articuloServicio.ObtenerSiguienteNroCodigo().ToString();
            txtcodigoBarra.Focus();

        }



        public override void EjecutarComandoEliminar()
        {
            _articuloServicio.Eliminar(EntidadId.Value);
        }

        private void btnNuevoIva_Click(object sender, System.EventArgs e)
        {
            var formularioNIva = new _00026_Abm_Iva(TipoOperacion.Nuevo);
            formularioNIva.ShowDialog();
            if (formularioNIva.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbIva, _ivaServicio.Obtener(string.Empty, false));
            }

        }

        private void btnNuevaMarca_Click(object sender, System.EventArgs e)
        {
            var formularioNMarca = new _00022_Abm_Marca(TipoOperacion.Nuevo);
            formularioNMarca.ShowDialog();
            if (formularioNMarca.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty, false));
            }


        }

        private void btnNuevoRubro_Click(object sender, System.EventArgs e)
        {
            var formularioNRubro = new _00020_Abm_Rubro(TipoOperacion.Nuevo);
            formularioNRubro.ShowDialog();
            if (formularioNRubro.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty, false));
            }
        }

        private void btnNuevaUnidad_Click(object sender, System.EventArgs e)
        {
            var formularioNUnidadMed = new _00024_Abm_UnidadDeMedida(TipoOperacion.Nuevo);
            formularioNUnidadMed.ShowDialog();
            if (formularioNUnidadMed.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbUnidad, _unidadMedidaServicio.Obtener(string.Empty, false));
            }
        }

        private void btnAgregarImagen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
                imgFoto.Image = Image.FromFile(openFile.FileName);

        }
    }
}
