using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using IServicios.BajaArticulo;
using IServicios.MotivoBaja;
using PresentacionBase.Formularios;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00029_BajaDeArticulos : FormConsulta
    {


        private readonly IBajaArticuloServicio _bajaArticuloServicio;

        private readonly IMotivoBajaServicio _motivoBajaArticulo;

        private readonly IArticuloServicio _articuloServicio;


        public _00029_BajaDeArticulos(ArticuloDto articuloDto, IMotivoBajaServicio motivoBajaArticulo, IArticuloServicio articuloServicio, IBajaArticuloServicio bajaArticuloServicio)
        {
            InitializeComponent();

            _motivoBajaArticulo = motivoBajaArticulo;
            _articuloServicio = articuloServicio;
            _bajaArticuloServicio = bajaArticuloServicio;


        }


        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {

            dgv.DataSource = _bajaArticuloServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }


        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv);

            dgv.Columns["Id"].Visible = false;
            dgv.Columns["Id"].Width = 50;
            dgv.Columns["Id"].HeaderText = "Id";
            dgv.Columns["Id"].DisplayIndex = 0;

            dgv.Columns["MotivoBaja"].Visible = true;
            dgv.Columns["MotivoBaja"].Width = 100;
            dgv.Columns["MotivoBaja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["MotivoBaja"].HeaderText = @"Motivo Baja";
            dgv.Columns["MotivoBaja"].DisplayIndex = 1;

            dgv.Columns["Cantidad"].Visible = true;
            dgv.Columns["Cantidad"].Width = 100;
            dgv.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Cantidad"].HeaderText = @"Cantidad";
            dgv.Columns["Cantidad"].DisplayIndex = 2;


            dgv.Columns["Fecha"].Visible = true;
            dgv.Columns["Fecha"].Width = 100;
            dgv.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Fecha"].HeaderText = @"Fecha";
            dgv.Columns["Fecha"].DisplayIndex = 3;


            dgv.Columns["Observacion"].Visible = true;
            dgv.Columns["Observacion"].Width = 80;
            dgv.Columns["Observacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Observacion"].HeaderText = @"Observacion";
            dgv.Columns["Observacion"].DisplayIndex = 4;


            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 80;
            dgv.Columns["EliminadoStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["EliminadoStr"].HeaderText = @"Eliminado";
            dgv.Columns["EliminadoStr"].DisplayIndex = 5;


        }

        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {

            var form = new _00030_Abm_BajaArticulos(tipoOperacion, id);
            form.ShowDialog();

            return form.RealizoAlgunaOperacion;

        }




    }
}
