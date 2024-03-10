using Microsoft.EntityFrameworkCore;

namespace ProiectTPBD
{
    public class CustomMessageBox : Form
    {
        public DialogResult Result { get; private set; }

        public CustomMessageBox(string text, string title)
        {
            this.Text = title;
            Label label = new Label() { Text = text, AutoSize = true };
            this.Controls.Add(label);

            Button yesButton = new Button() { Text = "Da", DialogResult = DialogResult.Yes };
            yesButton.Click += (sender, e) => { this.Result = DialogResult.Yes; this.Close(); };
            this.Controls.Add(yesButton);

            Button noButton = new Button() { Text = "Nu", DialogResult = DialogResult.No };
            noButton.Click += (sender, e) => { this.Result = DialogResult.No; this.Close(); };
            this.Controls.Add(noButton);

            this.Size = new Size(550, 260);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Center the label and buttons
            label.Location = new Point((this.ClientSize.Width - label.Width) / 2, 40);
            yesButton.Size = new Size(100, 40);
            yesButton.Location = new Point((this.ClientSize.Width - yesButton.Width * 2 - 10) / 2, label.Bottom + 40);
            noButton.Size = new Size(100, 40);
            noButton.Location = new Point(yesButton.Right + 10, label.Bottom + 40);
        }
    }


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

            //Database connection
            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.ToList();       

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

            dataGridView.CellBeginEdit += (cellSender, cellEventArgs) =>
            {
                if (dataGridView.Columns[cellEventArgs.ColumnIndex].Name != "Nume")
                {
                    cellEventArgs.Cancel = true;
                }
            };

            dataGridView.CellValueChanged += (cellSender, cellEventArgs) =>
            {
                try
                {
                    var db = new ProiectDbContext();
                    var angajat = (Angajat)dataGridView.Rows[cellEventArgs.RowIndex].DataBoundItem;
                    db.Entry(angajat).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            };

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
        }

        private void IESIREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = new CustomMessageBox("Vreti sa parasiti programul?", "Actiunea necesita confirmare").ShowDialog();
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
