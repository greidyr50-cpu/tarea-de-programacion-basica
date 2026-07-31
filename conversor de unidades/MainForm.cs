using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Conversor_de_unidades
{
    public class MainForm : Form
    {
        private TextBox inputTextBox;
        private ComboBox comboBox;
        private Button convertButton;
        private Label resultLabel;

        public MainForm()
        {
            Text = "Conversor de unidades";
            Size = new Size(420, 200);
            StartPosition = FormStartPosition.CenterScreen;

            inputTextBox = new TextBox { Location = new Point(20, 20), Width = 200 }; 

            comboBox = new ComboBox { Location = new Point(240, 20), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            comboBox.Items.AddRange(new object[] { "Km → Millas", "Millas → Km", "°C → °F", "°F → °C" });
            comboBox.SelectedIndex = 0;

            convertButton = new Button { Location = new Point(20, 60), Text = "Convertir", Width = 100 };
            convertButton.Click += ConvertButton_Click;

            resultLabel = new Label { Location = new Point(20, 100), AutoSize = true, Text = "Resultado: " };

            Controls.Add(inputTextBox);
            Controls.Add(comboBox);
            Controls.Add(convertButton);
            Controls.Add(resultLabel);
        }

        private void ConvertButton_Click(object? sender, EventArgs e)
        {
            var text = inputTextBox.Text.Trim();
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                // try with current culture
                if (!double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value))
                {
                    MessageBox.Show("Introduce un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string selected = comboBox.SelectedItem?.ToString() ?? string.Empty;
            double result = 0;
            string unit = string.Empty;

            switch (selected)
            {
                case "Km → Millas":
                    result = value * 0.621371;
                    unit = "mi";
                    break;
                case "Millas → Km":
                    result = value / 0.621371;
                    unit = "km";
                    break;
                case "°C → °F":
                    result = (value * 9.0 / 5.0) + 32.0;
                    unit = "°F";
                    break;
                case "°F → °C":
                    result = (value - 32.0) * 5.0 / 9.0;
                    unit = "°C";
                    break;
                default:
                    MessageBox.Show("Seleccione un tipo de conversión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            resultLabel.Text = $"Resultado: {result:G6} {unit}";
        }
    }
}
