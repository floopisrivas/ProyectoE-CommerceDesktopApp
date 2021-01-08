using Aplicacion.Constantes;
using IServicio.Persona.DTOs;
using IServicios.Comprobante.DTOs;
using IServicios.Comprobantes;
using IServicios.Comprobantes.DTOs;
using Presentacion.Core.Cliente;
using Presentacion.Core.Comprobantes.Clases;
using PresentacionBase.Formularios;
using StructureMap;
using System;
using System.Windows.Forms;
using Color = Aplicacion.Constantes.Color;

namespace Presentacion.Core.FormaPago
{
    public partial class _00044_FormaPago : FormBase
    {
        private FacturaView _factura;
        private ComprobantePendienteDto _facturaPendiente;
        private bool _vieneDeVentas;

        //private readonly IBancoServicio _bancoServicio;
        //private readonly ITarjetaServicio _tarjetaServicio;


        private readonly IFacturaServicio _facturaServicio;

        public bool RealizoVenta { get; set; } = false;

        public _00044_FormaPago()
        {
            InitializeComponent();

            _facturaServicio = ObjectFactory.GetInstance<IFacturaServicio>();

            
        }
        public _00044_FormaPago(FacturaView factura)
            : this()
        {
            _factura = factura;

            _vieneDeVentas = true;

            CargarDatos(_factura);
        }

        public _00044_FormaPago(ComprobantePendienteDto factura)
            :this()
        {
            _facturaPendiente = factura;
            _vieneDeVentas = false;

            CargarDatos(factura);

        }

        private void CargarDatos(ComprobantePendienteDto factura)
        {
            
            txtTotalAbonar.Text = factura.MontoPagarStr;
            nudMontoEfectivo.Value = factura.MontoPagar;

            nudMontoCheque.Value = 0;
            txtNumeroCheque.Clear();
            dtpFechaVencimientoCheque.Value = DateTime.Now;

            nudMontoCtaCte.Value = 0;
            txtApellido.Text = factura.Cliente;
            txtDni.Text = factura.Dni;
            txtTelefono.Text = factura.Telefono;
            txtDireccion.Text = factura.Direccion;

            nudMontoTarjeta.Value = 0;
            txtNumeroTarjeta.Clear();
            txtCuponPago.Clear();
            nudCantidadCuotas.Value = 1;


        }

        private void CargarDatos(FacturaView factura)
        {
            txtTotalAbonar.Text = factura.TotalStr;
            nudMontoEfectivo.Value = factura.Total;

            nudMontoCheque.Value = 0;
            nudMontoCtaCte.Value = 0;
            nudMontoTarjeta.Value = 0;
            txtApellido.Text = factura.Cliente.ApyNom;
            txtDni.Text = factura.Cliente.Dni;
            txtTelefono.Text = factura.Cliente.Telefono;
            txtDireccion.Text = factura.Cliente.Direccion;



        }

        private void nudMontoEfectivo_ValueChanged(object sender, EventArgs e)
        {
            nudTotalEfectivo.Value = nudMontoEfectivo.Value;
        }

        private void nudMontoCtaCte_ValueChanged(object sender, EventArgs e)
        {
            nudTotalCtaCte.Value = nudMontoCtaCte.Value;
        }

        private void nudTotalEfectivo_ValueChanged(object sender, EventArgs e)
        {
            nudTotal.Value = nudTotalCheque.Value + nudTotalEfectivo.Value + nudTotalCtaCte.Value
                + nudTotalTarjeta.Value;

        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var fLookUpCliente = ObjectFactory.GetInstance<ClienteLookUp>();
            fLookUpCliente.ShowDialog();
            if (fLookUpCliente.EntidadSeleccionada != null)
            {
                var _cliente = (ClienteDto)fLookUpCliente.EntidadSeleccionada;
                if (_cliente.ActivarCtaCte)
                {
                    txtApellido.Text = _cliente.ApyNom;
                    txtDni.Text = _cliente.Dni;
                    txtTelefono.Text = _cliente.Telefono;
                    txtDireccion.Text = _cliente.Direccion;
                    if (_vieneDeVentas)
                    {
                        _factura.Cliente = _cliente;
                    }
                    else
                    {
                        //FALTA ASIGNAR
                    }

                }
                else
                {
                    MessageBox.Show($"El cliente {_cliente.ApyNom} no tiene Activa la Cuenta Corriente", 
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }


        }

        private void nudPagaCon_ValueChanged(object sender, EventArgs e)
        {
            CalcularVuelto();
        }

        private void CalcularVuelto()
        {
            nudVuelto.Value = nudTotalEfectivo.Value - nudPagaCon.Value >= 0 ? 
                nudPagaCon.Value - nudTotalEfectivo.Value : (nudTotalEfectivo.Value - nudPagaCon.Value) * -1;
            nudVuelto.BackColor = nudTotalEfectivo.Value - nudPagaCon.Value >= 0 ? 
                System.Drawing.Color.Red : System.Drawing.Color.Green;

            nudVuelto.ForeColor = System.Drawing.Color.White;
        }

        private void nudTotal_ValueChanged(object sender, EventArgs e)
        {
            if (nudPagaCon.Value > 0)
            {
                CalcularVuelto();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            RealizoVenta = false;
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (nudTotal.Value > _factura.Total)
                {

                    if (MessageBox.Show("El total que esta por abonar es superior al monto a pagar.Desea continuar ? ", "Atención", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }

                }else if(nudTotal.Value < _factura.Total)
                {
                    MessageBox.Show("El total que esta por abonar es inferior al monto a pagar", "Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    return;

                }

                var _facturaNueva = new FacturaDto();
                {
                    if (_vieneDeVentas)
                    {
                        _facturaNueva.EmpleadoId = _factura.Vendedor.Id;
                        _facturaNueva.ClienteId = _factura.Cliente.Id;
                        _facturaNueva.TipoComprobante = _factura.TipoComprobante;
                        _facturaNueva.Descuento = _factura.Descuento;
                        _facturaNueva.SubTotal = _factura.SubTotal;
                        _facturaNueva.Total = _factura.Total;
                        _facturaNueva.Estado = Estado.Pagada;
                        _facturaNueva.PuestoTrabajoId = _factura.PuntoVentaId;
                        _facturaNueva.Fecha = DateTime.Now;
                        _facturaNueva.Iva105 = 0;
                        _facturaNueva.Iva21 = 0;
                        _facturaNueva.UsuarioId = _factura.UsuarioId;

                        foreach (var item in _factura.Items)
                        {
                            _facturaNueva.Items.Add(new DetalleComprobanteDto
                            {
                                Cantidad = item.Cantidad,
                                Iva = item.Iva,
                                Descripcion = item.Descripcion,
                                Precio = item.Precio,
                                ArticuloId = item.ArticuloId,
                                Codigo = item.CodigoBarra,
                                SubTotal = item.SubTotal,
                                Eliminado = false,
                            });

                        }

                    }
                    else
                    {
                        // TODO pendiente de Pago
                    }
                    if (nudTotalEfectivo.Value > 0)
                    {
                        _facturaNueva.FormasDePagos.Add(new FormaPagoDto
                        {
                            Monto = nudTotalEfectivo.Value,
                            TipoPago = TipoPago.Efectivo,
                            Eliminado = false

                        });

                    }
                    if (nudTotalCtaCte.Value > 0)
                    {
                        if (_vieneDeVentas)
                        {
                            // TODO: Falta ver deuda del Cliente
                            var deuda = 0;
                            if (_factura.Cliente.ActivarCtaCte)
                            {
                                if (_factura.Cliente.TieneLimiteCompra && _factura.Cliente.MontoMaximoCtaCte < deuda + nudTotalCtaCte.Value)
                                {
                                    var menssajeCtaCte = $"El cliente {_factura.Cliente.ApyNom} esta por arriba del limite Permitido." + Environment.NewLine +
                                        $" El limite es {_factura.Cliente.MontoMaximoCtaCte.ToString("C")}";

                                    MessageBox.Show(menssajeCtaCte, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;

                                }


                                _facturaNueva.FormasDePagos.Add(new FormaPagoCtaCteDto
                                {
                                    TipoPago = TipoPago.CtaCte,
                                    ClienteId = _factura.Cliente.Id,
                                    Monto = nudTotalCtaCte.Value,
                                    Eliminado = false,
                                });

                            }


                        }
                        else
                        {
                            // TODO: Facturas Pendientes de Pago
                        }
                    }
                    _facturaServicio.Insertar(_facturaNueva);
                    MessageBox.Show("Los datos se grabaron correctamente");

                    RealizoVenta = true;
                    Close();

                }


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                RealizoVenta = false;
            }

        }
    }
}
