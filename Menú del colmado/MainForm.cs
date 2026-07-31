using System;
using System.Drawing;
using System.Windows.Forms;

namespace Menú_del_colmado
{
    public class MainForm : Form
    {
        ComboBox comboProducts;
        TextBox txtQuantity;
        Label lblSubtotal, lblDiscount, lblTotal;
        RadioButton rbCash, rbCard;
        Button btnCalculate;
        ErrorProvider errorProvider;

        readonly (string name, decimal price)[] products = new[]
        {
            ("Arroz 1kg", 80.00m),
            ("Aceite 1L", 320.00m),
            ("Azúcar 1kg", 45.00m),
            ("Leche 1L", 80.00m)
        };

        public MainForm()
        {
            Text = "MENÚ DEL COLMADO";
            Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
            ClientSize = new Size(440, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            InitializeComponents();
        }

        void InitializeComponents()
        {
            errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            Label title = new Label { Text = "MENÚ DEL COLMADO", Font = new Font(FontFamily.GenericSansSerif, 12F, FontStyle.Bold), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(10, 8), Size = new Size(420, 24) };
            Controls.Add(title);

            Label lblProduct = new Label{ Text = "Producto:", Location = new Point(10,44), AutoSize = true };
            Controls.Add(lblProduct);

            comboProducts = new ComboBox{ Location = new Point(90,40), Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach(var p in products) comboProducts.Items.Add($"{p.name} - RD$ {p.price:N2}");
            if (comboProducts.Items.Count > 0) comboProducts.SelectedIndex = 0;
            Controls.Add(comboProducts);

            Label lblQty = new Label{ Text = "Cantidad:", Location = new Point(10,84), AutoSize = true };
            Controls.Add(lblQty);

            txtQuantity = new TextBox{ Location = new Point(90,80), Width = 140, Text = "1" };
            txtQuantity.TextChanged += (s,e) => UpdateTotal();
            txtQuantity.KeyPress += (s,e) => { if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))) e.Handled = true; };
            Controls.Add(txtQuantity);

            GroupBox gbPayment = new GroupBox{ Text = "Forma de pago", Location = new Point(10,120), Size = new Size(420,70) };
            rbCash = new RadioButton{ Text = "Efectivo (5% descuento)", Location = new Point(10,25), AutoSize = true };
            rbCard = new RadioButton{ Text = "Tarjeta", Location = new Point(250,25), AutoSize = true, Checked = true };
            rbCash.CheckedChanged += (s,e) => UpdateTotal();
            gbPayment.Controls.Add(rbCash);
            gbPayment.Controls.Add(rbCard);
            Controls.Add(gbPayment);

            btnCalculate = new Button{ Text = "Calcular", Location = new Point(10,205), Width = 100, Height = 30 };
            btnCalculate.Click += (s,e) => UpdateTotal();
            Controls.Add(btnCalculate);

            lblSubtotal = new Label{ Text = "Subtotal: RD$ 0.00", Location = new Point(130,205), AutoSize = false, Size = new Size(300,22), TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(lblSubtotal);

            lblDiscount = new Label{ Text = "Descuento: RD$ 0.00", Location = new Point(130,230), AutoSize = false, Size = new Size(300,22), TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(lblDiscount);

            lblTotal = new Label{ Text = "Total: RD$ 0.00", Location = new Point(130,255), AutoSize = false, Size = new Size(300,28), Font = new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle, TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(lblTotal);

            comboProducts.SelectedIndexChanged += (s,e) => UpdateTotal();

            UpdateTotal();
        }

        void UpdateTotal()
        {
            errorProvider.SetError(txtQuantity, string.Empty);

            if (comboProducts.SelectedIndex < 0)
            {
                lblSubtotal.Text = "Seleccione un producto";
                lblDiscount.Text = string.Empty;
                lblTotal.Text = string.Empty;
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                errorProvider.SetError(txtQuantity, "Ingrese una cantidad válida (número entero mayor que 0)");
                lblSubtotal.Text = "Subtotal: -";
                lblDiscount.Text = "Descuento: -";
                lblTotal.Text = "Total: -";
                lblTotal.ForeColor = Color.Red;
                return;
            }

            var product = products[comboProducts.SelectedIndex];
            decimal subtotal = product.price * qty;
            decimal discount = rbCash.Checked ? Math.Round(subtotal * 0.05m, 2) : 0m;
            decimal total = subtotal - discount;

            lblSubtotal.Text = $"Subtotal:    RD$ {subtotal:N2}";
            lblDiscount.Text = $"Descuento:   RD$ {discount:N2}";
            lblTotal.Text = $"Total:       RD$ {total:N2}";
            lblTotal.ForeColor = SystemColors.ControlText;
        }
    }
}
