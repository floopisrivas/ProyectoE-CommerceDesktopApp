using IServicios.Caja.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Presentacion.Core.Caja
{
    public partial class VerComprobantesCaja : Form
    {
        public VerComprobantesCaja(List<ComprobanteCajaDto> comprobantes)
        {
            InitializeComponent();
            dgvGrilla.DataSource = comprobantes.ToList();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
