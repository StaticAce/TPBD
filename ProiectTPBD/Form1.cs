using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CustomMessageBox
            // 
            ClientSize = new Size(1552, 563);
            Name = "CustomMessageBox";
            ResumeLayout(false);
        }
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            var db = new ProiectDbContext();

            aJUTORToolStripMenuItem.Click += AJUTORToolStripMenuItem_Click;
            iNTRODUCEREDATEToolStripMenuItem.Click += INTRODUCEREDATEToolStripMenuItem_Click;
            tIPARIREToolStripMenuItem.Click += TIPARIREToolStripMenuItem_Click;
            mODIFPROCENTEToolStripMenuItem.Click += MODIFPROCENTEToolStripMenuItem_Click;
            iESIREToolStripMenuItem.Click += IESIREToolStripMenuItem_Click;

            actualizareDateToolStripMenuItem.Click += ActualizareDateToolStripMenuItem_Click;
            adaugareAngajatiToolStripMenuItem.Click += AdaugareAngajatiToolStripMenuItem_Click;
            stergereAngajatiToolStripMenuItem.Click += StergereAngajatiToolStripMenuItem_Click;

            statPlataToolStripMenuItem.Click += StatPlataToolStripMenuItem_Click;
            fluturasiToolStripMenuItem.Click += FluturasiToolStripMenuItem_Click;

            panel1.Dock = DockStyle.Fill;

            //Mesaj initial -> Nici una din optiuni selectate
            panel1.Controls.Clear();
            Label label = new();
            label.Text = "Proiect TPBD";
            label.AutoSize = true;
            label.Location = new Point(700, 100);
            label.Font = new Font(label.Font.FontFamily, 60);
            label.TextAlign = ContentAlignment.TopCenter;
            panel1.Controls.Add(label);
        }

        private void FluturasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            var reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 0);
            panel1.Controls.Add(reportViewer);

            var stream = new MemoryStream(Reports.fluturasi);
            stream.Position = 0;

            var context = new ProiectDbContext();
            var data = context.Angajati;

            reportViewer.LocalReport.LoadReportDefinition(stream);
            reportViewer.LocalReport.DataSources
                .Add(new ReportDataSource("DataSet1", data));
            reportViewer.RefreshReport();
        }

        private void StatPlataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            var reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 0);
            panel1.Controls.Add(reportViewer);

            var stream = new MemoryStream(Reports.statdeplata);
            stream.Position = 0;

            var context = new ProiectDbContext();
            var data = context.Angajati;

            reportViewer.LocalReport.LoadReportDefinition(stream);
            reportViewer.LocalReport.DataSources
                .Add(new ReportDataSource("DataSet1", data));
            reportViewer.RefreshReport();
        }

        private void StergereAngajatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = new DataGridView();
            TextBox textBox = new TextBox();
            Button buttonSearch = new Button();
            Button buttonDelete = new Button(); // New delete button
            Label label = new Label();
            Label labelMessage = new Label(); // New label for messages

            // DataGridView
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = new Size(1750, panel1.Height);
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

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

            dataGridView.CellBeginEdit += (cellSender, cellEventArgs) =>
            {
                if (dataGridView.Columns[cellEventArgs.ColumnIndex].Name != "Nume")
                {
                    cellEventArgs.Cancel = true;
                }
            };

            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.ToList();

            // buttonDelete
            buttonDelete.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonDelete.Location = new Point(panel1.Width - buttonDelete.Width - 105, 200); // Adjust the location as needed
            buttonDelete.Size = new Size(150, 50);
            buttonDelete.Text = "Stergere";
            buttonDelete.Click += (deleteSender, deleteEventArgs) =>
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var confirmResult = new CustomMessageBox("Vreti sa stergeti angajatul selectat?", "Actiunea necesita confirmare").ShowDialog();
                    if (confirmResult == DialogResult.Yes)
                    {
                        var db = new ProiectDbContext();
                        var angajat = (Angajat)dataGridView.SelectedRows[0].DataBoundItem;
                        db.Angajati.Remove(angajat);
                        db.SaveChanges();
                        dataGridView.DataSource = db.Angajati.ToList();
                    }
                }
                else
                {
                    labelMessage.Text = "Nu ati selectat nici o persoana";
                    labelMessage.Visible = true;
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                    timer.Tick += (timerSender, timerEventArgs) =>
                    {
                        labelMessage.Visible = false;
                        timer.Stop();
                    };
                    timer.Start();
                }
            };

            // labelMessage
            labelMessage.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelMessage.Location = new Point(panel1.Width - labelMessage.Width - 270, 100); // Adjust the location as needed
            labelMessage.AutoSize = true;
            labelMessage.Visible = false;
            labelMessage.ForeColor = Color.Red;

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(buttonDelete);
            panel1.Controls.Add(label);
            panel1.Controls.Add(labelMessage);
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
            dataGridView.Size = new Size(1750, panel1.Height);
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // TextBox
            textBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Font = new Font(label.Font.FontFamily, 10);
            label.Text = "Cautati dupa o valoare:";
            label.AutoSize = true;
            textBox.Location = new Point(panel1.Width - textBox.Width - 80, 50);
            textBox.Size = new Size(150, 50);

            // buttonSearch
            buttonSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonSearch.Location = new Point(panel1.Width - buttonSearch.Width - 105, 105);
            buttonSearch.Size = new Size(150, 50);
            buttonSearch.Text = "Cautare";

            // Label
            label.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Location = new Point(panel1.Width - label.Width - 295, 50);
            label.Size = new Size(150, 50);
            label.Font = new Font(label.Font.FontFamily, 10);

            // Label for Nume
            Label labelNume = new Label();
            labelNume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelNume.Location = new Point(panel1.Width - labelNume.Width - 280, 200);
            labelNume.Size = new Size(150, 50);
            labelNume.Text = ("Nume:");
            labelNume.Font = new Font(labelNume.Font.FontFamily, 10);

            // TextBox for nume
            TextBox textBoxNume = new TextBox();
            textBoxNume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxNume.Location = new Point(panel1.Width - textBoxNume.Width - 80, 200);
            textBoxNume.Size = new Size(150, 50);

            // Label for Prenume
            Label labelPrenume = new Label();
            labelPrenume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelPrenume.Location = new Point(panel1.Width - labelPrenume.Width - 280, 250);
            labelPrenume.Size = new Size(150, 50);
            labelPrenume.Text = ("Prenume:");
            labelPrenume.Font = new Font(labelPrenume.Font.FontFamily, 10);

            // TextBox for Prenume
            TextBox textBoxPrenume = new TextBox();
            textBoxPrenume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxPrenume.Location = new Point(panel1.Width - textBoxPrenume.Width - 80, 250);
            textBoxPrenume.Size = new Size(150, 50);

            // Label for Functie
            Label labelFunctie = new Label();
            labelFunctie.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelFunctie.Location = new Point(panel1.Width - labelFunctie.Width - 280, 300);
            labelFunctie.Size = new Size(150, 50);
            labelFunctie.Text = ("Functie:");
            labelFunctie.Font = new Font(labelFunctie.Font.FontFamily, 10);

            // TextBox for Functie
            TextBox textBoxFunctie = new TextBox();
            textBoxFunctie.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxFunctie.Location = new Point(panel1.Width - textBoxFunctie.Width - 80, 300);
            textBoxFunctie.Size = new Size(150, 50);

            // Label for Salar_baza
            Label labelSalarBaza = new Label();
            labelSalarBaza.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelSalarBaza.Location = new Point(panel1.Width - labelSalarBaza.Width - 280, 350);
            labelSalarBaza.Size = new Size(150, 50);
            labelSalarBaza.Text = ("Salar de bază:");
            labelSalarBaza.Font = new Font(labelSalarBaza.Font.FontFamily, 10);

            // TextBox for Salar_baza
            TextBox textBoxSalarBaza = new TextBox();
            textBoxSalarBaza.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxSalarBaza.Location = new Point(panel1.Width - textBoxSalarBaza.Width - 80, 350);
            textBoxSalarBaza.Size = new Size(150, 50);

            // Label for Spor
            Label labelSpor = new Label();
            labelSpor.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelSpor.Location = new Point(panel1.Width - labelSpor.Width - 280, 400);
            labelSpor.Size = new Size(150, 50);
            labelSpor.Text = ("Spor:");
            labelSpor.Font = new Font(labelSpor.Font.FontFamily, 10);

            // TextBox for Spor
            TextBox textBoxSpor = new TextBox();
            textBoxSpor.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxSpor.Location = new Point(panel1.Width - textBoxSpor.Width - 80, 400);
            textBoxSpor.Size = new Size(150, 50);

            // Label for PremiiBrute
            Label labelPremiiBrute = new Label();
            labelPremiiBrute.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelPremiiBrute.Location = new Point(panel1.Width - labelPremiiBrute.Width - 280, 450);
            labelPremiiBrute.Size = new Size(150, 50);
            labelPremiiBrute.Text = ("Premii Brute:");
            labelPremiiBrute.Font = new Font(labelSpor.Font.FontFamily, 10);

            // TextBox for PremiiBrute
            TextBox textBoxPremiiBrute = new TextBox();
            textBoxPremiiBrute.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxPremiiBrute.Location = new Point(panel1.Width - textBoxPremiiBrute.Width - 80, 450);
            textBoxPremiiBrute.Size = new Size(150, 50);

            // Label for Retineri
            Label labelRetineri = new Label();
            labelRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelRetineri.Location = new Point(panel1.Width - labelRetineri.Width - 280, 500);
            labelRetineri.Size = new Size(150, 50);
            labelRetineri.Text = ("Retineri:");
            labelRetineri.Font = new Font(labelRetineri.Font.FontFamily, 10);

            // TextBox for Retineri
            TextBox textBoxRetineri = new TextBox();
            textBoxRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxRetineri.Location = new Point(panel1.Width - textBoxRetineri.Width - 80, 500);
            textBoxRetineri.Size = new Size(150, 50);

            // Button for adding new object
            Button buttonAdd = new Button();
            buttonAdd.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonAdd.Location = new Point(panel1.Width - buttonAdd.Width - 105, 600);
            buttonAdd.Size = new Size(150, 50);
            buttonAdd.Text = "Adaugare";

            // Label for error message
            Label labelError = new Label();
            labelError.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelError.Location = new Point(panel1.Width - labelError.Width - 80, 650);
            labelError.Size = new Size(150, 200);
            labelError.ForeColor = Color.Red;
            labelError.Font = new Font(labelError.Font.FontFamily, 10);

            var db = new ProiectDbContext();
            dataGridView.DataSource = db.Angajati.Select(a => new {
                a.NrCrt,
                a.Nume,
                a.Prenume,
                a.Functie,
                a.Salar_baza,
                a.Spor,
                a.Premii_brute,
                a.Total_brut,
                a.Brut_Impozitabil,
                a.Impozit,
                a.Cas,
                a.Cass,
                a.Retineri,
                a.Virat_Card
            }).ToList();

            textBoxSpor.Text = "0";
            textBoxPremiiBrute.Text = "0";
            textBoxRetineri.Text = "0";

            buttonSearch.Click += (searchSender, searchEventArgs) =>
            {
                string searchValue = textBox.Text.Trim().ToLower();

                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    var db = new ProiectDbContext();
                    var angajati = db.Angajati.ToList();
                    var initializare = db.Angajati.FirstOrDefault();

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

            buttonAdd.Click += (addSender, addEventArgs) =>
            {
                string nume = textBoxNume.Text.Trim();
                string prenume = textBoxPrenume.Text.Trim();
                string functie = textBoxFunctie.Text.Trim();
                string salarBazaStr = textBoxSalarBaza.Text.Trim();
                string sporStr = textBoxSpor.Text.Trim();

                int salarBaza, spor;

                if (!Regex.IsMatch(nume, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(nume) ||
                    !Regex.IsMatch(prenume, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(prenume) ||
                    !Regex.IsMatch(functie, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(functie) ||
                    !int.TryParse(salarBazaStr, out salarBaza) || !int.TryParse(sporStr, out spor) || (salarBaza < 3300) || (spor < 0))
                {
                    labelError.Text = "Datele introduse nu sunt valide!";
                    labelError.Visible = true;
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                    timer.Tick += (timerSender, timerEventArgs) =>
                    {
                        labelError.Visible = false;
                        timer.Stop();
                    };
                    timer.Start();
                }
                else
                {
                    var db = new ProiectDbContext();
                    var calculare = db.Calculare.FirstOrDefault();

                    int totalBrut = salarBaza + (int)(salarBaza * ((double)spor / 100));

                    int cas = (int)(totalBrut * ((double)calculare.Cas / 100));
                    int cass = (int)(totalBrut * ((double)calculare.Cass / 100));
                    int brutImpozabil = (int)(totalBrut - cas - cass);
                    int impozit = (int)(brutImpozabil * ((double)calculare.Impozit / 100));
                    int viratcard = totalBrut - impozit - cas - cass;

                    var angajat = new Angajat
                    {
                        Nume = nume,
                        Prenume = prenume,
                        Functie = functie,
                        Salar_baza = salarBaza,
                        Spor = spor,
                        Premii_brute = 0,
                        Total_brut = totalBrut,
                        Cas = cas,
                        Cass = cass,
                        Brut_Impozitabil = brutImpozabil,
                        Impozit = impozit,
                        Retineri = 0,
                        Virat_Card = viratcard,
                    };
                    db.Angajati.Add(angajat);
                    db.SaveChanges();

                    dataGridView.DataSource = db.Angajati.ToList();
                }
            };


            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
            panel1.Controls.Add(textBoxNume);
            panel1.Controls.Add(textBoxPrenume);
            panel1.Controls.Add(textBoxFunctie);
            panel1.Controls.Add(textBoxSalarBaza);
            panel1.Controls.Add(textBoxSpor);
            panel1.Controls.Add(textBoxPremiiBrute);
            panel1.Controls.Add(textBoxRetineri);
            panel1.Controls.Add(labelError);
            panel1.Controls.Add(buttonAdd);
            panel1.Controls.Add(labelNume);
            panel1.Controls.Add(labelPrenume);
            panel1.Controls.Add(labelFunctie);
            panel1.Controls.Add(labelSalarBaza);
            panel1.Controls.Add(labelSpor);
            panel1.Controls.Add(labelPremiiBrute);
            panel1.Controls.Add(labelRetineri);
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
            dataGridView.Size = new Size(1750, panel1.Height);
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // TextBox
            textBox.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Font = new Font(label.Font.FontFamily, 10);
            label.Text = "Cautati dupa o valoare:";
            label.AutoSize = true;
            textBox.Location = new Point(panel1.Width - textBox.Width - 80, 50);
            textBox.Size = new Size(150, 50);

            // buttonSearch
            buttonSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonSearch.Location = new Point(panel1.Width - buttonSearch.Width - 105, 105);
            buttonSearch.Size = new Size(150, 50);
            buttonSearch.Text = "Cautare";

            // Label
            label.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label.Location = new Point(panel1.Width - label.Width - 295, 50);
            label.Size = new Size(150, 50);
            label.Font = new Font(label.Font.FontFamily, 10);

            // Label for Nume
            Label labelNume = new Label();
            labelNume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelNume.Location = new Point(panel1.Width - labelNume.Width - 280, 200);
            labelNume.Size = new Size(150, 50);
            labelNume.Text = ("Nume:");
            labelNume.Font = new Font(labelNume.Font.FontFamily, 10);

            // TextBox for nume
            TextBox textBoxNume = new TextBox();
            textBoxNume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxNume.Location = new Point(panel1.Width - textBoxNume.Width - 80, 200);
            textBoxNume.Size = new Size(150, 50);

            // Label for Prenume
            Label labelPrenume = new Label();
            labelPrenume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelPrenume.Location = new Point(panel1.Width - labelPrenume.Width - 280, 250);
            labelPrenume.Size = new Size(150, 50);
            labelPrenume.Text = ("Prenume:");
            labelPrenume.Font = new Font(labelPrenume.Font.FontFamily, 10);

            // TextBox for Prenume
            TextBox textBoxPrenume = new TextBox();
            textBoxPrenume.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxPrenume.Location = new Point(panel1.Width - textBoxPrenume.Width - 80, 250);
            textBoxPrenume.Size = new Size(150, 50);

            // Label for Functie
            Label labelFunctie = new Label();
            labelFunctie.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelFunctie.Location = new Point(panel1.Width - labelFunctie.Width - 280, 300);
            labelFunctie.Size = new Size(150, 50);
            labelFunctie.Text = ("Functie:");
            labelFunctie.Font = new Font(labelFunctie.Font.FontFamily, 10);

            // TextBox for Functie
            TextBox textBoxFunctie = new TextBox();
            textBoxFunctie.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxFunctie.Location = new Point(panel1.Width - textBoxFunctie.Width - 80, 300);
            textBoxFunctie.Size = new Size(150, 50);

            // Label for Salar_baza
            Label labelSalarBaza = new Label();
            labelSalarBaza.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelSalarBaza.Location = new Point(panel1.Width - labelSalarBaza.Width - 280, 350);
            labelSalarBaza.Size = new Size(150, 50);
            labelSalarBaza.Text = ("Salar de bază:");
            labelSalarBaza.Font = new Font(labelSalarBaza.Font.FontFamily, 10);

            // TextBox for Salar_baza
            TextBox textBoxSalarBaza = new TextBox();
            textBoxSalarBaza.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxSalarBaza.Location = new Point(panel1.Width - textBoxSalarBaza.Width - 80, 350);
            textBoxSalarBaza.Size = new Size(150, 50);

            // Label for Spor
            Label labelSpor = new Label();
            labelSpor.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelSpor.Location = new Point(panel1.Width - labelSpor.Width - 280, 400);
            labelSpor.Size = new Size(150, 50);
            labelSpor.Text = ("Spor:");
            labelSpor.Font = new Font(labelSpor.Font.FontFamily, 10);

            // TextBox for Spor
            TextBox textBoxSpor = new TextBox();
            textBoxSpor.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxSpor.Location = new Point(panel1.Width - textBoxSpor.Width - 80, 400);
            textBoxSpor.Size = new Size(150, 50);

            // Label for Retineri
            Label labelRetineri = new Label();
            labelRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelRetineri.Location = new Point(panel1.Width - labelRetineri.Width - 280, 450);
            labelRetineri.Size = new Size(150, 50);
            labelRetineri.Text = ("Retineri:");
            labelRetineri.Font = new Font(labelRetineri.Font.FontFamily, 10);

            // TextBox for Retineri
            TextBox textBoxRetineri = new TextBox();
            textBoxRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxRetineri.Location = new Point(panel1.Width - textBoxRetineri.Width - 80, 450);
            textBoxRetineri.Size = new Size(150, 50);

            // Label for Premii Brute
            Label labelPremiiBrute = new Label();
            labelPremiiBrute.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelPremiiBrute.Location = new Point(panel1.Width - labelPremiiBrute.Width - 280, 500);
            labelPremiiBrute.Size = new Size(150, 50);
            labelPremiiBrute.Text = ("Premii brute:");
            labelPremiiBrute.Font = new Font(labelPremiiBrute.Font.FontFamily, 10);

            // TextBox for Premii Brute
            TextBox textBoxPremiiBrute = new TextBox();
            textBoxPremiiBrute.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBoxPremiiBrute.Location = new Point(panel1.Width - textBoxPremiiBrute.Width - 80, 500);
            textBoxPremiiBrute.Size = new Size(150, 50);

            // Button for adding new object
            Button buttonAdd = new Button();
            buttonAdd.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonAdd.Location = new Point(panel1.Width - buttonAdd.Width - 105, 550);
            buttonAdd.Size = new Size(150, 50);
            buttonAdd.Text = "Actualizare";

            // Label for error message
            Label labelError = new Label();
            labelError.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelError.Location = new Point(panel1.Width - labelError.Width - 80, 600);
            labelError.Size = new Size(150, 200);
            labelError.ForeColor = Color.Red;
            labelError.Font = new Font(labelError.Font.FontFamily, 10);

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

            dataGridView.SelectionChanged += (selectionSender, selectionEventArgs) =>
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                    textBoxNume.Text = selectedRow.Cells["Nume"].Value.ToString();
                    textBoxPrenume.Text = selectedRow.Cells["Prenume"].Value.ToString();
                    textBoxFunctie.Text = selectedRow.Cells["Functie"].Value.ToString();
                    textBoxSalarBaza.Text = selectedRow.Cells["Salar_baza"].Value.ToString();
                    textBoxSpor.Text = selectedRow.Cells["Spor"].Value.ToString();
                    textBoxRetineri.Text = selectedRow.Cells["Retineri"].Value.ToString();
                    textBoxPremiiBrute.Text = selectedRow.Cells["Premii_brute"].Value.ToString();
                }
            };

            buttonAdd.Click += (updateSender, updateEventArgs) =>
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int selectedNrCrt = Convert.ToInt32(selectedRow.Cells["NrCrt"].Value);

                    try
                    {
                        var db = new ProiectDbContext();
                        var angajatToUpdate = db.Angajati.FirstOrDefault(a => a.NrCrt == selectedNrCrt);
                        var calculare = db.Calculare.FirstOrDefault();

                        if (angajatToUpdate != null)
                        {
                            if ((Convert.ToInt32(textBoxRetineri.Text) - angajatToUpdate.Virat_Card) > 0)
                            {
                                labelError.Text = "Retinerile depasesc salarul!";
                                labelError.Visible = true;
                                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                                timer.Tick += (timerSender, timerEventArgs) =>
                                {
                                    labelError.Visible = false;
                                    timer.Stop();
                                };
                                timer.Start();
                            }
                            else
                            {
                                angajatToUpdate.Nume = textBoxNume.Text;
                                angajatToUpdate.Prenume = textBoxPrenume.Text;
                                angajatToUpdate.Functie = textBoxFunctie.Text;
                                angajatToUpdate.Salar_baza = Convert.ToInt32(textBoxSalarBaza.Text);
                                angajatToUpdate.Spor = Convert.ToInt32(textBoxSpor.Text);
                                angajatToUpdate.Retineri = Convert.ToInt32(textBoxRetineri.Text);
                                angajatToUpdate.Premii_brute = Convert.ToInt32(textBoxPremiiBrute.Text);

                                // Verificare pentru valorile negative de tip int
                                if (Convert.ToInt32(textBoxRetineri.Text) < 0 ||
                                    Convert.ToInt32(textBoxSalarBaza.Text) < 0 ||
                                    Convert.ToInt32(textBoxSpor.Text) < 0 ||
                                    Convert.ToInt32(textBoxPremiiBrute.Text) < 0)
                                {
                                    labelError.Text = "Retineri, SalarBaza, Spor si Premii brute nu pot avea valori negative!";
                                    labelError.Visible = true;
                                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                                    timer.Tick += (timerSender, timerEventArgs) =>
                                    {
                                        labelError.Visible = false;
                                        timer.Stop();
                                    };
                                    timer.Start();
                                }
                                else
                                {
                                    string nume = textBoxNume.Text.Trim();
                                    string prenume = textBoxPrenume.Text.Trim();
                                    string functie = textBoxFunctie.Text.Trim();

                                    if (!Regex.IsMatch(nume, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(nume) ||
                                        !Regex.IsMatch(prenume, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(prenume) ||
                                        !Regex.IsMatch(functie, @"^[A-Za-zăîâșțĂÎÂȘȚ\s]+$") || string.IsNullOrEmpty(functie))
                                    {
                                        labelError.Text = "Numele, prenumele și funcția trebuie să conțină doar litere și spații!";
                                        labelError.Visible = true;
                                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                                        timer.Tick += (timerSender, timerEventArgs) =>
                                        {
                                            labelError.Visible = false;
                                            timer.Stop();
                                        };
                                        timer.Start();
                                    }
                                    else
                                    {
                                        int totalBrut = angajatToUpdate.Salar_baza + (int)(angajatToUpdate.Salar_baza * ((double)angajatToUpdate.Spor / 100)) + (int)angajatToUpdate.Premii_brute;

                                        int cas = (int)(totalBrut * ((double)calculare.Cas / 100));
                                        int cass = (int)(totalBrut * ((double)calculare.Cass / 100));
                                        int brutImpozabil = (int)(totalBrut - cas - cass);
                                        int impozit = (int)(brutImpozabil * ((double)calculare.Impozit / 100));
                                        int viratcard = totalBrut - impozit - cas - cass - angajatToUpdate.Retineri;

                                        if (totalBrut < 3300)
                                        {
                                            labelError.Text = "Pragul pentru salar minim nu este atins!";
                                            labelError.Visible = true;
                                            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                                            timer.Tick += (timerSender, timerEventArgs) =>
                                            {
                                                labelError.Visible = false;
                                                timer.Stop();
                                            };
                                            timer.Start();
                                        }
                                        else
                                        {
                                            angajatToUpdate.Total_brut = totalBrut;
                                            angajatToUpdate.Cas = cas;
                                            angajatToUpdate.Cass = cass;
                                            angajatToUpdate.Brut_Impozitabil = brutImpozabil;
                                            angajatToUpdate.Impozit = impozit;
                                            angajatToUpdate.Virat_Card = viratcard;

                                            db.SaveChanges();
                                            dataGridView.DataSource = db.Angajati.ToList();

                                            MessageBox.Show("Datele au fost actualizate cu succes!");
                                        }
                                    }
                                }                             
                            }
                        }
                        else
                        {
                            MessageBox.Show("Angajatul nu a putut fi găsit în baza de date.");
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("A apărut o eroare la actualizarea angajatului: " + exc.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Nu a fost selectat niciun angajat pentru actualizare.");
                }
            };


            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(label);
            panel1.Controls.Add(textBoxNume);
            panel1.Controls.Add(textBoxPrenume);
            panel1.Controls.Add(textBoxFunctie);
            panel1.Controls.Add(textBoxSalarBaza);
            panel1.Controls.Add(textBoxSpor);
            panel1.Controls.Add(labelError);
            panel1.Controls.Add(buttonAdd);
            panel1.Controls.Add(labelNume);
            panel1.Controls.Add(labelPrenume);
            panel1.Controls.Add(labelFunctie);
            panel1.Controls.Add(labelSalarBaza);
            panel1.Controls.Add(labelSpor);
            panel1.Controls.Add(labelRetineri);
            panel1.Controls.Add(textBoxRetineri);
            panel1.Controls.Add(labelPremiiBrute);
            panel1.Controls.Add(textBoxPremiiBrute);
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
            DataGridView dataGridView = new DataGridView();
            TextBox textBox = new TextBox();
            Button buttonSearch = new Button();
            Label label = new Label();

            // Label for Nume
            Label labelCas = new Label();
            labelCas.Text = "Cas:";
            labelCas.AutoSize = true;
            labelCas.Font = new Font(labelCas.Font.FontFamily, 15);

            // TextBox for Nume
            TextBox textBoxCas = new TextBox();
            textBoxCas.Size = new Size(200, 60);

            // Label for Prenume
            Label labelCass = new Label();
            labelCass.Text = "Cass:";
            labelCass.AutoSize = true;
            labelCass.Font = new Font(labelCass.Font.FontFamily, 15);

            // TextBox for Prenume
            TextBox textBoxCass = new TextBox();
            textBoxCass.Size = new Size(200, 60);

            // Label for Functie
            Label labelImpozit = new Label();
            labelImpozit.Text = "Impozit:";
            labelImpozit.AutoSize = true;
            labelImpozit.Font = new Font(labelImpozit.Font.FontFamily, 15);

            // TextBox for Functie
            TextBox textBoxImpozit = new TextBox();
            textBoxImpozit.Size = new Size(200, 60);

            // Button for adding new object
            Button buttonAdd = new Button();
            buttonAdd.Text = "Actualizare";
            buttonAdd.Size = new Size(200, 60);

            // Label for error message
            Label labelError = new Label();
            labelError.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelError.Location = new Point(panel1.Width - labelError.Width - 1180, 670);
            labelError.Size = new Size(1000, 100);
            labelError.ForeColor = Color.Red;
            labelError.Font = new Font(labelError.Font.FontFamily, 15);

            // Label for error message
            Label labelSuccess = new Label();
            labelSuccess.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelSuccess.Location = new Point(panel1.Width - labelSuccess.Width - 1180, 770);
            labelSuccess.Size = new Size(1000, 100);
            labelSuccess.ForeColor = Color.Green;
            labelSuccess.Font = new Font(labelSuccess.Font.FontFamily, 15);

            // Calculate positions for controls
            int panelCenterX = panel1.Width / 2;
            int textBoxWidth = 300; // Increased width for text boxes
            int textBoxHeight = 80; // Increased height for text boxes
            int verticalSpacing = 40; // Increased vertical spacing
            int startY = 80; // Increased startY value

            labelCas.Location = new Point(panelCenterX - labelCas.Width - textBoxWidth, startY);
            textBoxCas.Location = new Point(panelCenterX, startY);

            labelCass.Location = new Point(panelCenterX - labelCass.Width - textBoxWidth, startY + textBoxHeight + verticalSpacing);
            textBoxCass.Location = new Point(panelCenterX, startY + textBoxHeight + verticalSpacing);

            labelImpozit.Location = new Point(panelCenterX - labelImpozit.Width - textBoxWidth, startY + 2 * (textBoxHeight + verticalSpacing));
            textBoxImpozit.Location = new Point(panelCenterX, startY + 2 * (textBoxHeight + verticalSpacing));

            // Adjust the position of the button
            int buttonY = startY + 3 * (textBoxHeight + verticalSpacing);
            buttonAdd.Location = new Point(panelCenterX - buttonAdd.Width / 2, buttonY);

            var db = new ProiectDbContext();
            var calculare = db.Calculare.FirstOrDefault();

            textBoxCas.Text = calculare.Cas.ToString();
            textBoxCass.Text = calculare.Cass.ToString();
            textBoxImpozit.Text = calculare.Impozit.ToString();

            buttonAdd.Click += (updateSender, updateEventArgs) =>
            {
                var db = new ProiectDbContext();
                var calculare = db.Calculare.FirstOrDefault();

                if (!int.TryParse(textBoxCas.Text, out int casValue) ||
                    !int.TryParse(textBoxCass.Text, out int cassValue) ||
                    !int.TryParse(textBoxImpozit.Text, out int impozitValue) ||
                    ((casValue + cassValue + impozitValue) > 99))
                {
                    labelError.Text = "Datele introduse nu sunt valide sau suma procentelor depaseste 99!";
                    labelError.Visible = true;
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                    timer.Tick += (timerSender, timerEventArgs) =>
                    {
                        labelError.Visible = false;
                        timer.Stop();
                    };
                    timer.Start();

                    return;
                }

                // Proceed with updating the database
                calculare.Cas = casValue;
                calculare.Cass = cassValue;
                calculare.Impozit = impozitValue;

                var confirmResult = new CustomMessageBox("Vreti sa modificati datele?", "Actiunea necesita confirmare").ShowDialog();
                if (confirmResult == DialogResult.Yes)
                {
                    if (((calculare.Cas + calculare.Cass + calculare.Impozit) < 100))
                    {
                        foreach (var angajatToUpdate in db.Angajati)
                        {
                            int totalBrut = angajatToUpdate.Salar_baza + (int)(angajatToUpdate.Salar_baza * ((double)angajatToUpdate.Spor / 100)) + (int)angajatToUpdate.Premii_brute;

                            int cas = (int)(totalBrut * ((double)calculare.Cas / 100));
                            int cass = (int)(totalBrut * ((double)calculare.Cass / 100));
                            int brutImpozabil = (int)(totalBrut - cas - cass);
                            int impozit = (int)(brutImpozabil * ((double)calculare.Impozit / 100));
                            int viratcard = totalBrut - impozit - cas - cass - angajatToUpdate.Retineri;

                            angajatToUpdate.Total_brut = totalBrut;
                            angajatToUpdate.Cas = cas;
                            angajatToUpdate.Cass = cass;
                            angajatToUpdate.Brut_Impozitabil = brutImpozabil;
                            angajatToUpdate.Impozit = impozit;
                            angajatToUpdate.Virat_Card = viratcard;
                        }

                        db.SaveChanges();

                        labelSuccess.Text = "Datele au fost modificate!";
                        labelSuccess.Visible = true;
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 3000 };
                        timer.Tick += (timerSender, timerEventArgs) =>
                        {
                            labelSuccess.Visible = false;
                            timer.Stop();
                        };
                        timer.Start();
                    }
                    else
                    {
                        MessageBox.Show("Datele introduse nu sunt corecte!.");
                    }
                }
            };


            panel1.Controls.Clear();
            panel1.Controls.Add(textBoxCas);
            panel1.Controls.Add(textBoxCass);
            panel1.Controls.Add(textBoxImpozit);
            panel1.Controls.Add(labelError);
            panel1.Controls.Add(labelSuccess);
            panel1.Controls.Add(buttonAdd);
            panel1.Controls.Add(labelCas);
            panel1.Controls.Add(labelCass);
            panel1.Controls.Add(labelImpozit);
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
            label.Text = "Aplicația permite gestionarea datelor salariale a angajaților. Funcționalitățile principale includ:\n\n\n" +
                         "- Actualizare date: actualizarea informațiilor pentru un angajat sau mai mulți, inclusiv premii brute și retineri.\n" +
                         "- Adăugare angajați: adăugarea unor noi angajați cu validare și calcul automat al salariului.\n" +
                         "- Ștergere angajați: căutarea și ștergerea angajaților după nume sau prenume.\n" +
                         "- Stat plata: generarea și tipărirea statului de plată pentru toți angajații, inclusiv opțiunea de salvare în fișier PDF.\n" +
                         "- Modificare Impozit: modificarea procentelor de impozitare cu protecție și recalculare automată a salariilor.\n" +
                         "- Iesire: închiderea programului.";
            label.AutoSize = true;
            label.Font = new Font(label.Font.FontFamily, 17);

            // Calculează coordonatele pentru a centra label-ul pe ecran
            label.Location = new Point(300, 380);

            panel1.Controls.Add(label);
        }


    }
}
