using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using IServicios.Articulo.DTOs;
using PresentacionBase.Formularios;
using StructureMap;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Presentacion.Core.Articulo
{
    public partial class ArticuloLookUp : FormLookUp
    {
        private readonly IArticuloServicio _articuloServicio;
        private long _listaPrecioId;

        public ArticuloDto ArticuloSeleccionado => (ArticuloDto)EntidadSeleccionada;
        public ArticuloLookUp(long listaPrecioId)
        {
            InitializeComponent();
            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
            _listaPrecioId = listaPrecioId;
        }



        public override void ActualizarDatos(DataGridView dgv, string cadenaBuscar)
        {
            dgv.DataSource = (List<ArticuloVentaDto>) _articuloServicio
                .ObtenerLookUp(cadenaBuscar, _listaPrecioId);

            FormatearGrilla(dgv);

        }


        public override void FormatearGrilla(DataGridView dgv)
        {
            
            base.FormatearGrilla(dgv);

            dgv.Columns["CodigoBarra"].Visible = true;
            dgv.Columns["CodigoBarra"].Width = 80;
            dgv.Columns["CodigoBarra"].HeaderText = @"Codigo Barra";
            dgv.Columns["CodigoBarra"].DisplayIndex = 0;


            dgv.Columns["Descripcion"].Visible = true;
            dgv.Columns["Descripcion"].Width = 100;
            dgv.Columns["Descripcion"].HeaderText = @"Descripcion";
            dgv.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Descripcion"].DisplayIndex = 1;


            dgv.Columns["Stock"].Visible = true;
            dgv.Columns["Stock"].HeaderText = @"Stock";
            dgv.Columns["Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["Stock"].Width = 250;
            dgv.Columns["Stock"].DisplayIndex = 2;

            dgv.Columns["PrecioStr"].Visible = true;
            dgv.Columns["PrecioStr"].Width = 65;
            dgv.Columns["PrecioStr"].HeaderText = @"Precio Publico";
            dgv.Columns["PrecioStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns["PrecioStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["PrecioStr"].DisplayIndex = 3;


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
