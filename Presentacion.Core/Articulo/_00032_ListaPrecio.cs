using IServicio.ListaPrecio;
using PresentacionBase.Formularios;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo
{
    public partial class _00032_ListaPrecio : FormConsulta
    {
        private readonly IListaPrecioServicio _listaServicio;

        public _00032_ListaPrecio(IListaPrecioServicio listaServicio)
        {
            InitializeComponent();
            _listaServicio = listaServicio;

        }

        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = _listaServicio.Obtener(cadenaBuscar);

            base.ActualizarDatos(dgv, cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView dgv)
        {
            base.FormatearGrilla(dgv); 

            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].HeaderText = @"Descripción";

            dgv.Columns["PorcentajeGanancia"].Visible = true;
            dgv.Columns["PorcentajeGanancia"].Width = 70;
            dgv.Columns["PorcentajeGanancia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["PorcentajeGanancia"].HeaderText = "Porcentaje Ganancia %";

            dgv.Columns["NecesitaAutorizacion"].Visible = true;
            dgv.Columns["NecesitaAutorizacion"].Width = 100;
            dgv.Columns["NecesitaAutorizacion"].HeaderText = "Necesita Autorizacion";
            dgv.Columns["NecesitaAutorizacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["NecesitaAutorizacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["EliminadoStr"].Visible = true;
            dgv.Columns["EliminadoStr"].Width = 100;
            dgv.Columns["EliminadoStr"].HeaderText = "Eliminado";



        }


        public override bool EjecutarComando(TipoOperacion tipoOperacion, long? id = null)
        {
            var formulario = new _00033_Abm_ListaPrecio(tipoOperacion, id);

            formulario.ShowDialog();

            return base.EjecutarComando(tipoOperacion, id);
        }


    }
}
