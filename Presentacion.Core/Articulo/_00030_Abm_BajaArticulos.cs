using IServicio.Articulo;
using IServicio.Articulo.DTOs;
using IServicios.BajaArticulo;
using IServicios.BajaArticulo.DTOs;
using IServicios.MotivoBaja;
using PresentacionBase.Formularios;
using StructureMap;
using System.Windows.Forms;
using Aplicacion.Constantes;
using System;
using IServicios.Articulo.DTOs;
using System.Linq;

namespace Presentacion.Core.Articulo
{
    public partial class _00030_Abm_BajaArticulos : FormAbm
    {

        private ArticuloDto _articuloSeleccionado;

        private readonly IBajaArticuloServicio _bajaArticuloServicio;
        private readonly IMotivoBajaServicio _motivoBajaArticulo;
        private readonly IArticuloServicio _articuloServicio;
        private long articuloId;

        public _00030_Abm_BajaArticulos(TipoOperacion tipoOperacion, long? entidadId = null, IMotivoBajaServicio motivoBajaArticulo = null, IArticuloServicio articuloServicio = null,
            IBajaArticuloServicio bajaArticuloServicio = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _motivoBajaArticulo = ObjectFactory.GetInstance<IMotivoBajaServicio>();
            _articuloServicio = ObjectFactory.GetInstance<IArticuloServicio>();
            _bajaArticuloServicio = ObjectFactory.GetInstance<IBajaArticuloServicio>();

            _articuloSeleccionado = null;
        }



        public override void CargarDatos(long? entidadId)
        {

            base.CargarDatos(entidadId);

            if(entidadId.HasValue)
            {
                var res = (BajaArticuloDto)_bajaArticuloServicio.Obtener(entidadId.Value);
                var resArticulo = (ArticuloDto)_articuloServicio.Obtener(res.ArticuloId);
                if(res == null)
                {
                    MessageBox.Show("Ocurrio un error en la baja del articulo seleccionado");
                    
                }

                txtArticulo.Text = res.ArticuloId.ToString();
                nudStockActual.Value = resArticulo.StockActual;
                imgFotoArticulo.Image = Imagen.ConvertirImagen(resArticulo.Foto);
                nudCantidadBaja.Value = res.Cantidad;
                txtObservacion.Text = res.Observacion;


                PoblarComboBox(cmbMotivoBaja, _motivoBajaArticulo.Obtener(string.Empty), "Descripcion", "Id");

                cmbMotivoBaja.SelectedValue = res.MotivoBajaId;


                if (TipoOperacion == TipoOperacion.Eliminar)
                    DesactivarControles(this);

            }
            else
            {

                PoblarComboBox(cmbMotivoBaja, _motivoBajaArticulo.Obtener(string.Empty), "Descripcion", "Id");
                btnEjecutar.Text = "Nuevo";
                LimpiarControles(this, false);
            }


        }

        private void btnBuscarArticulo_Click(object sender, System.EventArgs e)
        {

           // var flookUpArticulo = ObjectFactory.GetInstance<ArticuloLookUp>();
            var flookUpArticulo = new ArticuloLookUp(1);
            flookUpArticulo.ShowDialog();

            if ((ArticuloDto)flookUpArticulo.ArticuloSeleccionado != null)
            {
                _articuloSeleccionado = (ArticuloDto)flookUpArticulo.ArticuloSeleccionado;
                txtArticulo.Text = _articuloSeleccionado.Id.ToString();
                 nudStockActual.Value = _articuloSeleccionado.StockActual;
                 imgFotoArticulo.Image = Imagen.ConvertirImagen(_articuloSeleccionado.Foto);
                

            }
           

        
        }

     


        public override void EjecutarComandoNuevo()
        {
            if (VerificarDatosObligatorios()) 
            { 

                    if (nudStockActual.Value >= nudCantidadBaja.Value)
                    {
                
                        var nuevaBaja = new BajaArticuloDto
                        {
                            ArticuloId = int.Parse(txtArticulo.Text),
                            MotivoBajaId = (long)cmbMotivoBaja.SelectedValue,
                            Cantidad = nudCantidadBaja.Value,
                            Fecha = DateTime.Now,
                            Observacion = txtObservacion.Text,
                            Eliminado = false
                    

                        };

                          _bajaArticuloServicio.Insertar(nuevaBaja);
               
                    }
                    else
                    {
                        MessageBox.Show("La cantidad de articulos a dar de baja debe ser menor o igual al stock actual");
                    }

            }
        }


        public override void EjecutarComandoModificar()
        {

           
                var modificarBaja = new BajaArticuloDto
                {
                    Id = EntidadId.Value,
                    ArticuloId = int.Parse(txtArticulo.Text),
                    MotivoBajaId = (long)cmbMotivoBaja.SelectedValue,
                    Cantidad = nudCantidadBaja.Value,
                    Observacion = txtObservacion.Text,
                    Eliminado = false
                };


                _bajaArticuloServicio.Modificar(modificarBaja);
            
        }

        public override void EjecutarComandoEliminar()
        {
            _bajaArticuloServicio.Eliminar(EntidadId.Value);
        }

        private void btnNuevoMotivoBaja_Click(object sender, EventArgs e)
        {
            var formularioNuevoMotivoBaja = new _00028_Abm_MotivoBaja(TipoOperacion.Nuevo);
            formularioNuevoMotivoBaja.ShowDialog();

            if(formularioNuevoMotivoBaja.RealizoAlgunaOperacion)
            {
                PoblarComboBox(cmbMotivoBaja, _motivoBajaArticulo.Obtener(string.Empty, false));

            }
        }
        public override bool VerificarDatosObligatorios()
        {
            if (nudCantidadBaja.Value<=0) return false;
            if (nudStockActual.Value <= 0) return false;
            if (string.IsNullOrEmpty(txtObservacion.Text)) return false;
            if (string.IsNullOrEmpty(txtArticulo.Text)) return false;
            if (cmbMotivoBaja.Items.Count <= 0) return false;
            return true;
        }
    }
}
