namespace ProiectTPBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var db = new ProiectDbContext();

            var firstEmployee = db.Employees.OrderBy(x=>x.Id).Last();

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
            throw new NotImplementedException();
        }
    }
}
