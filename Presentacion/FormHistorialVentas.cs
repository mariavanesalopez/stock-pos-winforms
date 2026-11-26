using Negocio;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public class FormHistorialVentas : Form
    {
        private readonly VentaServicio servicio = new VentaServicio();
        private readonly DataGridView grilla = new DataGridView();
        private readonly Button btnTicket = new Button();

        public FormHistorialVentas()
        {
            EstiloFormulario.Aplicar(this, "Ventas", "Consultá las ventas realizadas y sus tickets");
            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "id_venta", DataPropertyName = "id_venta", HeaderText = "N° Venta", FillWeight = 12 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "fecha_hora", DataPropertyName = "fecha_hora", HeaderText = "Fecha", FillWeight = 24 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "id_cliente", DataPropertyName = "id_cliente", HeaderText = "Cliente", FillWeight = 12 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "total", DataPropertyName = "total", HeaderText = "Total", FillWeight = 16, DefaultCellStyle = { Format = "C2" } });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "estado", DataPropertyName = "estado", HeaderText = "Estado", FillWeight = 16 });
            grilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(grilla);
            btnTicket.Text = "Ver ticket";
            btnTicket.Name = "btnVerTicket";
            btnTicket.Click += btnTicket_Click;
            Controls.Add(btnTicket);
            Resize += AjustarLayout;
            Shown += delegate { AjustarLayout(this, EventArgs.Empty); };
            Load += delegate { CargarVentas(); };
            AjustarLayout(this, EventArgs.Empty);
        }

        private void AjustarLayout(object sender, EventArgs e)
        {
            int alto = Parent == null ? ClientSize.Height : Parent.ClientSize.Height;
            int yBoton = Math.Min(410, Math.Max(260, alto - 72));
            grilla.SetBounds(24, 20, Math.Max(100, ClientSize.Width - 48), Math.Max(100, yBoton - 32));
            btnTicket.SetBounds(Math.Max(24, ClientSize.Width - 144), yBoton, 120, 36);
            btnTicket.BringToFront();
        }

        private void CargarVentas()
        {
            try
            {
                grilla.DataSource = servicio.ListarVentas();
            }
            catch (Exception ex) { MessageBox.Show("No se pudieron cargar las ventas: " + ex.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            if (grilla.CurrentRow == null) { MessageBox.Show("Seleccione una venta para ver su ticket."); return; }
            try
            {
                int id = Convert.ToInt32(grilla.CurrentRow.Cells["id_venta"].Value);
                using (FormVenta visor = new FormVenta())
                    visor.MostrarTicketDesdeHistorial(id);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}
