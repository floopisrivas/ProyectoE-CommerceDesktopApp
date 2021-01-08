using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using PresentacionBase.Formularios;
using System.Windows.Forms;
using Aplicacion.Constantes;
using System.Collections.Generic;
using IServicios.Articulo.DTOs;
using IServicio.ListaPrecio;

namespace Presentacion.Core.Articulo
{
    public partial class _00017_Articulo : FormConsultaConDetalle
    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        

        public _00017_Articulo(IArticuloServicio articuloServicio, IListaPrecioServicio listaPrecioServicio)
        {
            InitializeComponent();
            _articuloServicio = articuloServicio;
            _listaPrecioServicio = listaPrecioServicio;
        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            
            dgv.DataSource = _articuloServicio.Obtener(cadenaBuscar);

            dgvStock.DataSource = new List<StockDepositoDto>();

            dgvPrecios.DataSource = new List<PreciosDto>();


            base.ActualizarDatos(dgv, cadenaBuscar);
        }


        public override void FormatearGrilla(DataGridView dgv)
        {
            
            base.FormatearGrilla(dgv);

            FormatearGrillaPrecios(dgvPrecios); 

            FormatearGrillaStock(dgvStock);

            dgv.Columns["Codigo"].Visible = true;
            dgv.Columns["Codigo"].Width = 100;
            dgv.Columns["Codigo"].HeaderText = "Código";
            dgv.Columns["Codigo"].DisplayIndex = 0;


            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].Width = 180;
            dgv.Columns["CodigoBarra"].HeaderText = "Código Barra";
            dgv.Columns["CodigoBarra"].DisplayIndex = 1;


            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";
            dgv.Columns["Descripcion"].Width = 350;
            dgv.Columns["Descripcion"].DisplayIndex = 2;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 90;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";
            dgv.Columns["EliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["EliminadoStr"].DisplayIndex = 3;


            
        }

        public void FormatearGrillaStock(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);
            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 70;
            dgv.Columns["Cantidad"].HeaderText = "Cantidad";

            dgv.Columns["Desposito"].Visible = true;
            dgv.Columns["Desposito"].HeaderText = "Deposito";
            dgv.Columns["Desposito"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void FormatearGrillaPrecios(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);
            dgv.Columns["ListaPrecio"].Visible = true;
            dgv.Columns["ListaPrecio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["ListaPrecio"].HeaderText = "Lista Precio";

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].HeaderText = "Precio";
            dgv.Columns["PrecioStr"].Width = 100;


            dgv.Columns["FechaStr"].Visible = true;
            dgv.Columns["FechaStr"].HeaderText = "Fecha Act.";
            dgv.Columns["FechaStr"].Width = 100;
        }




        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00018_Abm_Articulo(tipoOperacion, id);

            formulario.ShowDialog();

            return formulario.RealizoAlgunaOperacion;
        }

        public override void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            base.dgvGrilla_RowEnter(sender, e);
            if (EntidadSeleccionada == null)
            {
                txtMarca.Clear();
                txtIva.Clear();
                txtRubro.Clear();
                txtUnidad.Clear();
                txtUbicacion.Clear();
                imgFoto.Image = null;

                dgvStock.DataSource = new List<StockDepositoDto>();
                dgvPrecios.DataSource = new List<PreciosDto>();


                return;
            }
            var articulo = (ArticuloDto)EntidadSeleccionada;
            txtMarca.Text = articulo.Marca;
            txtIva.Text = articulo.Iva;
            txtRubro.Text = articulo.Rubro;
            txtUnidad.Text = articulo.UnidadMedida;
            txtUbicacion.Text = articulo.Ubicacion;
            imgFoto.Image = Imagen.ConvertirImagen(articulo.Foto);
            // ================================================== //
             dgvStock.DataSource = articulo.Stocks;
            dgvPrecios.DataSource = articulo.Precios;
            nudStockActual.Value = articulo.StockActual;



        }

       
      
    }

}
