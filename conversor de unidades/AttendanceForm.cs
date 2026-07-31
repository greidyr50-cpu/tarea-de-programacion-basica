using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace conversor_de_unidades
{
    public class AttendanceForm : Form
    {
        private ListBox lstStudents;
        private CheckBox chkPresent;
        private Button btnResumen;
        private Dictionary<string, bool> attendance = new Dictionary<string, bool>();

        public AttendanceForm()
        {
            Text = "Control de asistencia";
            Size = new Size(400, 350);

            lstStudents = new ListBox { Location = new Point(10, 10), Size = new Size(240, 280) };
            lstStudents.SelectedIndexChanged += LstStudents_SelectedIndexChanged;

            chkPresent = new CheckBox { Text = "Presente", Location = new Point(260, 20), AutoSize = true };
            chkPresent.CheckedChanged += ChkPresent_CheckedChanged;

            btnResumen = new Button { Text = "Generar resumen", Location = new Point(260, 60), Size = new Size(110, 30) };
            btnResumen.Click += BtnResumen_Click;

            Controls.Add(lstStudents);
            Controls.Add(chkPresent);
            Controls.Add(btnResumen);

            LoadStudents();
        }

        private void LoadStudents()
        {
            // Lista de ejemplo; modificar según necesidad
            var students = new[] { "Ana", "Bruno", "Carla", "Diego", "Elena" };
            foreach (var s in students)
            {
                attendance[s] = false; // por defecto ausente
                lstStudents.Items.Add(s);
            }
        }

        private void LstStudents_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstStudents.SelectedItem is string name)
            {
                if (attendance.TryGetValue(name, out var present))
                    chkPresent.Checked = present;
            }
            else
            {
                chkPresent.Checked = false;
            }
        }

        private void ChkPresent_CheckedChanged(object? sender, EventArgs e)
        {
            if (lstStudents.SelectedItem is string name)
            {
                attendance[name] = chkPresent.Checked;
            }
        }

        private void BtnResumen_Click(object? sender, EventArgs e)
        {
            var present = attendance.Where(kv => kv.Value).Select(kv => kv.Key).ToList();
            var total = attendance.Count;
            var presentes = present.Count;

            var message = "Resumen de asistentes:\n";
            message += $"Total: {total}  Presentes: {presentes}\n\n";
            if (presentes > 0)
                message += string.Join("\n", present);
            else
                message += "Nadie marcado como presente.";

            MessageBox.Show(message, "Resumen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
