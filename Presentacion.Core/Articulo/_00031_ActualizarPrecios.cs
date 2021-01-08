using IServicio.Articulo;
using IServicio.ListaPrecio;
using IServicio.Marca;
using IServicio.Rubro;
using IServicios.Precio;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Aplicacion.Constantes.Clases.ValidacionDatosEntrada;

namespace Presentacion.Core.Articulo
{
    
    public partial class _00031_ActualizarPrecios : FormBase

    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IPrecioServicio _precioServicio;


        public _00031_ActualizarPrecios(IArticuloServicio articuloServicio, IMarcaServicio marcaServicio,
            IRubroServicio rubroServicio, IListaPrecioServicio listaPrecioServicio, IPrecioServicio precioServicio)
        {
            InitializeComponent();


            _articuloServicio = articuloServicio;
            _marcaServicio = marcaServicio;
            _rubroServicio = rubroServicio;
            _listaPrecioServicio = listaPrecioServicio;
            _precioServicio = precioServicio;


            PoblarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");

            PoblarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");

            PoblarComboBox(cmbListaPrecio, _listaPrecioServicio.Obtener(string.Empty), "Descripcion", "Id");

            AsignarEvento_EnterLeave(this);
           
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea limpiar los datos cargados", "Atención", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                LimpiarControles(this);
            }

        }

        private bool VerificarDatosObligatorios()
        {
            if (nudValor.Value <= 0) return false;


            return true;
        }



        private void btnEjecutar_Click(object sender, EventArgs e)
        {

            if (VerificarDatosObligatorios())
            {
                _precioServicio.Actualizar(chkMarca.Checked, chkRubro.Checked,
                    chkArticulo.Checked, chkListaPrecio.Checked, nudValor.Value, rdbPorcentaje.Checked, (long)cmbMarca.SelectedValue,
                   (long)cmbRubro.SelectedValue, (long)nudCodigoDesde.Value, (long)nudCodigoHasta.Value, (long)cmbListaPrecio.SelectedValue );

                MessageBox.Show("Se realizo la actualizacion de precio del artirculo.");
                Close();
            }
            else
            {
                MessageBox.Show("Por favor ingrese los datos Obligatorios");
            }


        }
    }
}
