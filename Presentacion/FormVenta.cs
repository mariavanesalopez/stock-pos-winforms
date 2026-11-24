using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormVenta : Form
    {
        private readonly VentaServicio ventaServicio = new VentaServicio();
        private readonly ClienteServicio clienteServicio = new ClienteServicio();
        private readonly ArticuloServicio articuloServicio = new ArticuloServicio();
        private int? ultimaVentaRegistrada;
        private TicketVenta ticketParaImprimir;

        public FormVenta()
        {
            InitializeComponent();
            EstiloFormulario.Aplicar(this, "Ventas", "Registrá una venta y consultá su comprobante");
            Resize += FormVenta_Resize;
            Shown += FormVenta_Shown;
            FormVenta_Resize(this, EventArgs.Empty);
        }

        private void FormVenta_Shown(object sender, EventArgs e)
        {
            AjustarDiseño(ClientSize.Width, Parent == null ? ClientSize.Height : Parent.ClientSize.Height);
        }

        private void FormVenta_Resize(object sender, EventArgs e)
        {
            AjustarDiseño(ClientSize.Width, ClientSize.Height);
        }

        private void AjustarDiseño(int ancho, int alto)
        {
            int margen = 24;
            int anchoContenido = Math.Max(200, ancho - margen * 2);
            int yCabecera = 12;
            label1.Location = new Point(margen, yCabecera);
            lblNVenta.Location = new Point(margen + 80, yCabecera);
            label2.Location = new Point(margen + 250, yCabecera);
            lblFecha.Location = new Point(margen + 300, yCabecera);

            label3.Location = new Point(margen, 48);
            cbxCliente.Location = new Point(margen, 68);
            cbxCliente.Width = Math.Min(440, anchoContenido);

            label4.Location = new Point(margen, 112);
            cbxArticulo.Location = new Point(margen, 132);
            cbxArticulo.Width = Math.Max(120, anchoContenido - 260);
            label5.Location = new Point(cbxArticulo.Right + 16, 112);
            updCantidad.Location = new Point(cbxArticulo.Right + 16, 132);
            btnAgregar.Location = new Point(updCantidad.Right + 16, 130);

            label6.Location = new Point(margen, 178);
            // El formulario se muestra embebido y su ClientSize no coincide siempre
            // con la altura visible del panel principal. Mantener acciones en zona segura.
            int yAcciones = 410;
            int altoGrilla = Math.Max(70, Math.Min(alto - 294, yAcciones - 208));
            dgvDetalleVenta.SetBounds(margen, 200, anchoContenido, altoGrilla);
            dgvDetalleVenta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnQuitar.Location = new Point(Math.Max(margen, ancho - margen - btnQuitar.Width), dgvDetalleVenta.Bottom + 4);

            int anchoBoton = Math.Max(80, Math.Min(112, (anchoContenido - 180) / 4));
            int separacionBoton = 10;
            btnNueva.SetBounds(margen, yAcciones, anchoBoton, 34);
            btnCobrar.SetBounds(margen + anchoBoton + separacionBoton, yAcciones, anchoBoton, 34);
            btnCancelar.SetBounds(margen + (anchoBoton + separacionBoton) * 2, yAcciones, anchoBoton, 34);
            btnTicket.SetBounds(margen + (anchoBoton + separacionBoton) * 3, yAcciones, anchoBoton, 34);

            int xTotal = Math.Max(margen + (anchoBoton + separacionBoton) * 4 + 20, ancho - margen - 180);
            label9.Location = new Point(xTotal, yAcciones - 20);
            txtTotal.SetBounds(xTotal, yAcciones, Math.Max(100, ancho - margen - xTotal), 30);
            btnNueva.BringToFront();
            btnCobrar.BringToFront();
            btnCancelar.BringToFront();
            btnTicket.BringToFront();
            txtTotal.BringToFront();
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {
            try
            {
                lblNVenta.Text = "Nueva venta";
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                cbxCliente.DataSource = clienteServicio.ListarClientes();
                cbxCliente.DisplayMember = "nombre";
                cbxCliente.ValueMember = "id_cliente";
                cbxCliente.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxArticulo.DataSource = articuloServicio.ListarArticulos();
                cbxArticulo.DisplayMember = "nombre";
                cbxArticulo.ValueMember = "id_articulo";
                cbxArticulo.DropDownStyle = ComboBoxStyle.DropDownList;
                updCantidad.Minimum = 1;
                updCantidad.Maximum = 100000;
                txtTotal.ReadOnly = true;
                if (cbxArticulo.DataSource is DataTable tabla && tabla.Rows.Count > 0)
                    cbxArticulo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo preparar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCobrar.Enabled = false;
                btnAgregar.Enabled = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxArticulo.SelectedValue == null || cbxArticulo.SelectedValue is DataRowView)
            {
                MessageBox.Show("Seleccione un artículo.");
                return;
            }

            int idArticulo = Convert.ToInt32(cbxArticulo.SelectedValue);
            int cantidad = Decimal.ToInt32(updCantidad.Value);
            try
            {
                Articulo articulo = articuloServicio.ObtenerArticuloPorId(idArticulo);
                int filaExistente = dgvDetalleVenta.Rows.Cast<DataGridViewRow>()
                    .ToList().FindIndex(fila => Convert.ToInt32(fila.Cells["colIdArticulo"].Value) == idArticulo);
                int cantidadAcumulada = cantidad;
                if (filaExistente >= 0)
                    cantidadAcumulada += Convert.ToInt32(dgvDetalleVenta.Rows[filaExistente].Cells["colCantidad"].Value);
                if (cantidadAcumulada > articulo.StockActual)
                {
                    MessageBox.Show("Stock insuficiente. Disponible: " + articulo.StockActual + ".", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (filaExistente >= 0)
                {
                    dgvDetalleVenta.Rows[filaExistente].Cells["colCantidad"].Value = cantidadAcumulada;
                    dgvDetalleVenta.Rows[filaExistente].Cells["colSubtotal"].Value = cantidadAcumulada * articulo.PrecioVenta;
                }
                else
                {
                    dgvDetalleVenta.Rows.Add(idArticulo, articulo.Nombre, cantidad, articulo.PrecioVenta, cantidad * articulo.PrecioVenta);
                }

                ActualizarTotalEnPantalla();
                cbxCliente.Enabled = true;
                updCantidad.Value = 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Artículo", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ActualizarTotalEnPantalla()
        {
            decimal total = dgvDetalleVenta.Rows.Cast<DataGridViewRow>()
                .Sum(fila => fila.Cells["colSubtotal"].Value == null ? 0m : Convert.ToDecimal(fila.Cells["colSubtotal"].Value));
            txtTotal.Text = total.ToString("F2", CultureInfo.CurrentCulture);
        }

        private void QuitarFilaSeleccionada()
        {
            if (dgvDetalleVenta.CurrentRow == null || dgvDetalleVenta.CurrentRow.IsNewRow) return;
            dgvDetalleVenta.Rows.Remove(dgvDetalleVenta.CurrentRow);
            ActualizarTotalEnPantalla();
        }

        private void dgvDetalleVenta_CellClick(object sender, DataGridViewCellEventArgs e) { }

        private void dgvDetalleVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvDetalleVenta.Columns[e.ColumnIndex].Name != "colCantidad") return;
            DataGridViewRow fila = dgvDetalleVenta.Rows[e.RowIndex];
            int cantidad;
            if (!int.TryParse(Convert.ToString(fila.Cells["colCantidad"].Value), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un entero mayor a cero.");
                fila.Cells["colCantidad"].Value = 1;
                cantidad = 1;
            }
            decimal precio = Convert.ToDecimal(fila.Cells["colPrecioUnitario"].Value);
            int idArticulo = Convert.ToInt32(fila.Cells["colIdArticulo"].Value);
            Articulo articulo = articuloServicio.ObtenerArticuloPorId(idArticulo);
            if (cantidad > articulo.StockActual)
            {
                MessageBox.Show("Stock insuficiente. Disponible: " + articulo.StockActual + ".");
                fila.Cells["colCantidad"].Value = articulo.StockActual > 0 ? Math.Min(articulo.StockActual, 1) : 1;
                cantidad = Convert.ToInt32(fila.Cells["colCantidad"].Value);
            }
            fila.Cells["colSubtotal"].Value = cantidad * precio;
            ActualizarTotalEnPantalla();
        }

        private void dgvDetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { QuitarFilaSeleccionada(); e.Handled = true; }
        }

        private void btnQuitar_Click_1(object sender, EventArgs e) { QuitarFilaSeleccionada(); }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (DataGridViewRow fila in dgvDetalleVenta.Rows)
                {
                    detalles.Add(new DetalleVenta(
                        Convert.ToInt32(fila.Cells["colIdArticulo"].Value),
                        Convert.ToInt32(fila.Cells["colCantidad"].Value),
                        Convert.ToDecimal(fila.Cells["colPrecioUnitario"].Value)));
                }

                int? idCliente = null;
                if (cbxCliente.SelectedValue != null && !(cbxCliente.SelectedValue is DataRowView))
                    idCliente = Convert.ToInt32(cbxCliente.SelectedValue);

                bool registrado = ventaServicio.RegistrarVenta(DateTime.Now, idCliente, detalles);
                if (!registrado) throw new InvalidOperationException("No se pudo registrar la venta.");
                ultimaVentaRegistrada = ventaServicio.ListarVentas().First().id_venta;
                MessageBox.Show("Venta registrada correctamente.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTicket.Enabled = true;
                LimpiarFormularioNuevaVenta();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "No se pudo cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LimpiarFormularioNuevaVenta()
        {
            dgvDetalleVenta.Rows.Clear();
            cbxCliente.Enabled = true;
            cbxCliente.SelectedIndex = cbxCliente.Items.Count > 0 ? 0 : -1;
            cbxArticulo.SelectedIndex = cbxArticulo.Items.Count > 0 ? 0 : -1;
            updCantidad.Value = 1;
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ActualizarTotalEnPantalla();
            cbxCliente.Focus();
        }

        private void btnNueva_Click(object sender, EventArgs e) { LimpiarFormularioNuevaVenta(); }
        private void btnCancelar_Click(object sender, EventArgs e) { Close(); }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ultimaVentaRegistrada.HasValue)
                    throw new InvalidOperationException("Primero debe cobrar una venta para poder mostrar su ticket.");

                TicketVenta ticket = ventaServicio.ObtenerTicket(ultimaVentaRegistrada.Value);
                MostrarTicket(ticket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarTicket(TicketVenta ticket)
        {
            Form vista = new Form
            {
                Text = "Ticket de venta " + ticket.IdVenta,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(520, 620),
                MinimumSize = new Size(450, 500)
            };
            TextBox contenido = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                WordWrap = false,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10f),
                Text = FormatearTicket(ticket)
            };
            Button imprimir = new Button
            {
                Text = "Imprimir / Guardar como PDF",
                Dock = DockStyle.Bottom,
                Height = 42
            };
            imprimir.Click += delegate { ImprimirTicket(ticket); };
            vista.Controls.Add(contenido);
            vista.Controls.Add(imprimir);
            vista.ShowDialog(this);
        }

        private static string FormatearTicket(TicketVenta ticket)
        {
            string linea = new string('-', 48);
            System.Text.StringBuilder texto = new System.Text.StringBuilder();
            texto.AppendLine("                 COMPROBANTE DE VENTA");
            texto.AppendLine(linea);
            texto.AppendLine("Venta: " + ticket.IdVenta);
            texto.AppendLine("Fecha: " + ticket.FechaHora.ToString("dd/MM/yyyy HH:mm:ss"));
            texto.AppendLine("Cliente: " + ticket.Cliente);
            texto.AppendLine("Estado: " + ticket.Estado);
            texto.AppendLine(linea);
            texto.AppendLine("Artículo                 Cant.   Precio    Subtotal");
            texto.AppendLine(linea);
            foreach (LineaTicketVenta detalle in ticket.Detalles)
            {
                string nombre = detalle.Articulo.Length > 22 ? detalle.Articulo.Substring(0, 22) : detalle.Articulo;
                texto.AppendLine(string.Format(CultureInfo.CurrentCulture, "{0,-22} {1,4} {2,9:N2} {3,10:N2}", nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.Subtotal));
            }
            texto.AppendLine(linea);
            texto.AppendLine("TOTAL: " + ticket.Total.ToString("C2", CultureInfo.CurrentCulture));
            texto.AppendLine();
            texto.AppendLine("              Gracias por su compra");
            return texto.ToString();
        }

        private void ImprimirTicket(TicketVenta ticket)
        {
            ticketParaImprimir = ticket;
            using (PrintDocument documento = new PrintDocument())
            using (PrintDialog dialogo = new PrintDialog())
            {
                documento.PrintPage += Documento_PrintPage;
                dialogo.Document = documento;
                dialogo.UseEXDialog = true;
                if (dialogo.ShowDialog(this) == DialogResult.OK) documento.Print();
            }
        }

        private void Documento_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font fuente = new Font("Consolas", 10f))
                e.Graphics.DrawString(FormatearTicket(ticketParaImprimir), fuente, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top);
            e.HasMorePages = false;
        }
    }
}
