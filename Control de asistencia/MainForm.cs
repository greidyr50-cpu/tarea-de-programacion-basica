using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Control_de_asistencia
{
    public class MainForm : Form
    {
        private ListBox lstStudents;
        private CheckBox chkPresente;
        private Button btnResumen;

        private List<string> originalNames = new List<string>
        {
            "Ana Pérez",
            "Carlos López",
            "María García",
            "Juan Martínez",
            "Lucía Fernández"
        };

        private HashSet<int> present = new HashSet<int>();

        public MainForm()
        {
            Text = "Control de asistencia";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(400, 300);

            InitializeComponents();
            LoadStudents();
        }

        private void InitializeComponents()
        {
            lstStudents = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(260, 240)
            };
            lstStudents.SelectedIndexChanged += LstStudents_SelectedIndexChanged;

            chkPresente = new CheckBox
            {
                Text = "Presente",
                Location = new Point(280, 20),
                AutoSize = true,
                Enabled = false
            };
            chkPresente.CheckedChanged += ChkPresente_CheckedChanged;

            btnResumen = new Button
            {
                Text = "Generar resumen",
                Location = new Point(280, 60),
                Size = new Size(100, 30)
            };
            btnResumen.Click += BtnResumen_Click;

            Controls.Add(lstStudents);
            Controls.Add(chkPresente);
            Controls.Add(btnResumen);
        }

        private void LoadStudents()
        {
            lstStudents.Items.Clear();
            foreach (var name in originalNames)
                lstStudents.Items.Add(name);
        }

        private void LstStudents_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = lstStudents.SelectedIndex;
            if (idx < 0)
            {
                chkPresente.Enabled = false;
                chkPresente.Checked = false;
                return;
            }
            chkPresente.Enabled = true;
            chkPresente.Checked = present.Contains(idx);
        }

        private void ChkPresente_CheckedChanged(object? sender, EventArgs e)
        {
            int idx = lstStudents.SelectedIndex;
            if (idx < 0) return;

            if (chkPresente.Checked)
                present.Add(idx);
            else
                present.Remove(idx);

            UpdateListItem(idx);
        }

        private void UpdateListItem(int idx)
        {
            string text = originalNames[idx];
            if (present.Contains(idx))
                text += " (Presente)";
            lstStudents.Items[idx] = text;
        }

        private void BtnResumen_Click(object? sender, EventArgs e)
        {
            if (present.Count == 0)
            {
                MessageBox.Show("No hay asistentes marcados.", "Resumen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var attendees = present.OrderBy(i => i).Select(i => originalNames[i]).ToList();
            string message = $"Asistentes ({attendees.Count}):\n" + string.Join("\n", attendees);
            MessageBox.Show(message, "Resumen de asistencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
