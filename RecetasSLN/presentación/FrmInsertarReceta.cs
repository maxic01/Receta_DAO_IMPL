using RecetasSLN.datos;
using RecetasSLN.dominio;
using RecetasSLN.servicios;
using RecetasSLN.servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN.presentación
{
    public partial class FrmInsertarReceta : Form
    {
        private Receta nueva;
        private IServicio servicio;

        public FrmInsertarReceta()
        {
            InitializeComponent();
            nueva = new Receta();
            servicio = new ImplementacionServicioF().crearServicio();
            
        }

        private void ultimaReceta()
        {
            lblNro.Text = "Receta #: " + servicio.proximaReceta();
        }

        private void FrmInsertarReceta_Load(object sender, EventArgs e)
        {
            ultimaReceta();
            cargarCombo();
            limpiar();
        }

        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtNombre.Focus();
            txtCheff.Text = string.Empty;
            cboTipo.Text = string.Empty;
            dgvDetalles.Rows.Clear();
            lblTotalIngredientes.Text = "Total de ingredientes:";
            ultimaReceta();
        }

        private void cargarCombo()
        {
            DataTable tabla = servicio.listarIngredientes();
            cboProducto.DataSource = tabla;
            cboProducto.ValueMember = "id_ingrediente";
            cboProducto.DisplayMember = "n_ingrediente";
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un ingrediente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(nudCantidad.Text) || !int.TryParse(nudCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["Ingrediente"].Value.ToString().Equals(cboProducto.Text))
                {
                    MessageBox.Show("Este ingrediente ya está cargado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }


            DataRowView item = (DataRowView)cboProducto.SelectedItem;
            int ingr = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();

            Ingrediente i = new Ingrediente(ingr, nom);
            int cant = Convert.ToInt32(nudCantidad.Value);
            DetalleReceta detalle = new DetalleReceta(i, cant);

            nueva.AgregarReceta(detalle);

            dgvDetalles.Rows.Add(new object[] { ingr, nom, cant });

            TotalIngredientes();
        }

        private void TotalIngredientes()
        {
            lblTotalIngredientes.Text = "Total de ingredientes:" + dgvDetalles.Rows.Count;
        }
        private bool existe(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["producto"].Value.Equals(text))
                    return true;
            }
            return false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCheff.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Chef", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCheff.Focus();
                return;
            }

            if (dgvDetalles.Rows.Count < 3)
            {
                MessageBox.Show("Debe ingresar 3 ingredientes como minimo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProducto.Focus();
                return;

            }
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un nombre", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNombre.Focus();
                return;
            }
            nueva.RecetaNro = servicio.proximaReceta();
            nueva.Nombre = txtNombre.Text;
            nueva.Chef = txtCheff.Text;
            nueva.TipoReceta = Convert.ToInt32(cboTipo.SelectedIndex);
            if (servicio.ejecutarSP(nueva))
            {
                MessageBox.Show("Receta guardada");
                limpiar();

            }
            else
            {
                MessageBox.Show("Receta NO guardada");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
