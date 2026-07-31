using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Registro_de_clientes
{
    public class MainForm : Form
    {
        private Label lblNombre;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private Button btnAgregar;
        private ListBox lstClientes;
        private ErrorProvider errorProvider;

        public MainForm()
        {
            Text = "Registro de clientes";
            Width = 600;
            Height = 420;
            StartPosition = FormStartPosition.CenterScreen;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            errorProvider = new ErrorProvider();

            lblNombre = new Label { Text = "Nombre:", AutoSize = true, Location = new Point(20, 20) };
            txtNombre = new TextBox { Location = new Point(100, 16), Width = 440 }; 

            lblTelefono = new Label { Text = "Teléfono:", AutoSize = true, Location = new Point(20, 60) };
            txtTelefono = new TextBox { Location = new Point(100, 56), Width = 200 };

            lblCorreo = new Label { Text = "Correo:", AutoSize = true, Location = new Point(20, 100) };
            txtCorreo = new TextBox { Location = new Point(100, 96), Width = 300 };

            btnAgregar = new Button { Text = "Agregar", Location = new Point(100, 140), Width = 120 };
            btnAgregar.Click += BtnAgregar_Click;

            lstClientes = new ListBox { Location = new Point(20, 190), Width = 540, Height = 170 };

            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(btnAgregar);
            Controls.Add(lstClientes);

            AcceptButton = btnAgregar;

            // Simple double-click to remove
            lstClientes.DoubleClick += (s, e) =>
            {
                if (lstClientes.SelectedIndex >= 0 && MessageBox.Show("Eliminar cliente seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lstClientes.Items.RemoveAt(lstClientes.SelectedIndex);
                }
            };
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            errorProvider.Clear();

            var nombre = txtNombre.Text.Trim();
            var telefono = txtTelefono.Text.Trim();
            var correo = txtCorreo.Text.Trim();

            bool valido = true;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (!IsValidPhone(telefono))
            {
                errorProvider.SetError(txtTelefono, "Teléfono no válido. Sólo dígitos, espacios, + o -.");
                valido = false;
            }

            if (!IsValidEmail(correo))
            {
                errorProvider.SetError(txtCorreo, "Correo no válido.");
                valido = false;
            }

            if (!valido)
            {
                return;
            }

            var display = $"{nombre}  —  {telefono}  —  {correo}";
            lstClientes.Items.Add(display);

            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtNombre.Focus();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Simple regex for basic validation
                return Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Allow digits, spaces, +, -, parentheses
            return Regex.IsMatch(phone, @"^[0-9+()\-\s]+$");
        }
    }
}
