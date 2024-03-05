namespace ProiectTPBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var db = new ProiectDbContext();

            var firstEmployee = db.Angajati.OrderBy(x=>x.Id).Last();

            //MessageBox.Show(firstEmployee.Nume.ToString());

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
            DataGridView dataGridView = new DataGridView();
            TextBox textBox = new TextBox();
            Button buttonSearch = new Button();
            Label label = new Label();

            // DataGridView
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = new Size(700, panel1.Height);

            // TextBox
            textBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Font = new Font(label.Font.FontFamily, 10);
            label.Text = "Introduceti un nume: ";
            label.AutoSize = true;
            textBox.Location = new Point(panel1.Width - textBox.Width - 80, 50);
            textBox.Size = new Size(150, 50);

            // buttonSearch
            buttonSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonSearch.Location = new Point(panel1.Width - buttonSearch.Width - 105, 150);
            buttonSearch.Size = new Size(150, 50);
            buttonSearch.Text = "Cautare";

            // Label
            label.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Location = new Point(panel1.Width - label.Width - 270, 50);
            label.Size = new Size(150, 50);

            buttonSearch.Click += (searchSender, searchEventArgs) =>
            {
                string searchValue = textBox.Text.Trim().ToLower();

                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    var db = new ProiectDbContext();
                    var angajati = db.Angajati.ToList();

                    List<Angajat> filteredAngajati = new List<Angajat>();

                    foreach (var angajat in angajati)
                    {
                        foreach (var property in angajat.GetType().GetProperties())
                        {
                            var value = property.GetValue(angajat);
                            if (value != null && value.ToString().ToLower().Contains(searchValue))
                            {
                                filteredAngajati.Add(angajat);
                                break;
                            }
                        }
                    }

                    dataGridView.DataSource = filteredAngajati;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            };

            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.ToList();

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
        }

        private void AdaugareAngajatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = new DataGridView();
            TextBox textBox = new TextBox();
            Button buttonSearch = new Button();
            Label label = new Label();

            // DataGridView
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = new Size(700, panel1.Height);

            // TextBox
            textBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Font = new Font(label.Font.FontFamily, 10);
            label.Text = "Introduceti un nume: ";
            label.AutoSize = true;
            textBox.Location = new Point(panel1.Width - textBox.Width - 80, 50);
            textBox.Size = new Size(150, 50);

            // buttonSearch
            buttonSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonSearch.Location = new Point(panel1.Width - buttonSearch.Width - 105, 150);
            buttonSearch.Size = new Size(150, 50);
            buttonSearch.Text = "Cautare";

            // Label
            label.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Location = new Point(panel1.Width - label.Width - 270, 50);
            label.Size = new Size(150, 50);

            buttonSearch.Click += (searchSender, searchEventArgs) =>
            {
                string searchValue = textBox.Text.Trim().ToLower();

                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    var db = new ProiectDbContext();
                    var angajati = db.Angajati.ToList();

                    List<Angajat> filteredAngajati = new List<Angajat>();

                    foreach (var angajat in angajati)
                    {
                        foreach (var property in angajat.GetType().GetProperties())
                        {
                            var value = property.GetValue(angajat);
                            if (value != null && value.ToString().ToLower().Contains(searchValue))
                            {
                                filteredAngajati.Add(angajat);
                                break;
                            }
                        }
                    }

                    dataGridView.DataSource = filteredAngajati;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            };

            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.ToList();

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
        }

        private void ActualizareDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = new DataGridView();
            TextBox textBox = new TextBox();
            Button buttonSearch = new Button();
            Label label = new Label();

            // DataGridView
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = new Size(700, panel1.Height);

            // TextBox
            textBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Font = new Font(label.Font.FontFamily, 10);
            label.Text = "Introduceti un nume: ";
            label.AutoSize = true;
            textBox.Location = new Point(panel1.Width - textBox.Width - 80, 50);
            textBox.Size = new Size(150, 50);

            // buttonSearch
            buttonSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonSearch.Location = new Point(panel1.Width - buttonSearch.Width - 105, 150);
            buttonSearch.Size = new Size(150, 50);
            buttonSearch.Text = "Cautare";

            // Label
            label.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Location = new Point(panel1.Width - label.Width - 270, 50);
            label.Size = new Size(150, 50);

            buttonSearch.Click += (searchSender, searchEventArgs) =>
            {
                string searchValue = textBox.Text.Trim().ToLower();

                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    var db = new ProiectDbContext();
                    var angajati = db.Angajati.ToList();

                    List<Angajat> filteredAngajati = new List<Angajat>();

                    foreach (var angajat in angajati)
                    {
                        foreach (var property in angajat.GetType().GetProperties())
                        {
                            var value = property.GetValue(angajat);
                            if (value != null && value.ToString().ToLower().Contains(searchValue))
                            {
                                filteredAngajati.Add(angajat);
                                break;
                            }
                        }
                    }

                    dataGridView.DataSource = filteredAngajati;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            };

            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.ToList();

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
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
