namespace ProiectTPBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var db = new ProiectDbContext();

            var firstEmployee = db.Angajati.OrderBy(x=>x.Id).Last();

            MessageBox.Show(firstEmployee.Nume.ToString());

            aJUTORToolStripMenuItem.Click += AJUTORToolStripMenuItem_Click;
            iNTRODUCEREDATEToolStripMenuItem.Click += INTRODUCEREDATEToolStripMenuItem_Click;
            tIPARIREToolStripMenuItem.Click += TIPARIREToolStripMenuItem_Click;
            mODIFPROCENTEToolStripMenuItem.Click += MODIFPROCENTEToolStripMenuItem_Click;
            iESIREToolStripMenuItem.Click += IESIREToolStripMenuItem_Click;

            actualizareDateToolStripMenuItem.Click += ActualizareDateToolStripMenuItem_Click;
            adaugareAngajatiToolStripMenuItem.Click += AdaugareAngajatiToolStripMenuItem_Click;
            stergereAngajatiToolStripMenuItem.Click += StergereAngajatiToolStripMenuItem_Click;
            calculSalariiToolStripMenuItem.Click += CalculSalariiToolStripMenuItem_Click;

            statPlataToolStripMenuItem.Click += StatPlataToolStripMenuItem_Click;
            fluturasiToolStripMenuItem.Click += FluturasiToolStripMenuItem_Click;

            //Mesaj initial -> Nici una din optiuni selectate
            panel1.Controls.Clear();
            Label label = new();
            label.Text = "Mesaj Initial";
            label.AutoSize = true;
            label.Location = new Point(2, 2);
            label.Font = new Font(label.Font.FontFamily, 10);
            panel1.Controls.Add(label);
        }

        private void FluturasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StatPlataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CalculSalariiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StergereAngajatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AdaugareAngajatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ActualizareDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IESIREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Vreti sa parasiti programul?",
                                     "Actiunea necesita confirmare",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MODIFPROCENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TIPARIREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void INTRODUCEREDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AJUTORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Label label = new();
            label.Text = "Testing testing 123";
            label.AutoSize = true;
            label.Location = new Point(2, 2);
            label.Font = new Font(label.Font.FontFamily, 10);
            panel1.Controls.Add(label);
        }
    }
}
