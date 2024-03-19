﻿using Microsoft.EntityFrameworkCore;
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
            ClientSize = new Size(278, 244);
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
            calculSalariiToolStripMenuItem.Click += CalculSalariiToolStripMenuItem_Click;

            statPlataToolStripMenuItem.Click += StatPlataToolStripMenuItem_Click;
            fluturasiToolStripMenuItem.Click += FluturasiToolStripMenuItem_Click;

            panel1.Dock = DockStyle.Fill;

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
            Button buttonDelete = new Button(); // New delete button
            Label label = new Label();
            Label labelMessage = new Label(); // New label for messages

            // DataGridView
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = new Size(1750, panel1.Height);
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

            //// Label for Retineri
            //Label labelRetineri = new Label();
            //labelRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //labelRetineri.Location = new Point(panel1.Width - labelRetineri.Width - 280, 450);
            //labelRetineri.Size = new Size(150, 50);
            //labelRetineri.Text = ("Retineri:");
            //labelRetineri.Font = new Font(labelRetineri.Font.FontFamily, 10);

            //// TextBox for Retineri
            //TextBox textBoxRetineri = new TextBox();
            //textBoxRetineri.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //textBoxRetineri.Location = new Point(panel1.Width - textBoxRetineri.Width - 80, 450);
            //textBoxRetineri.Size = new Size(150, 50);

            // Button for adding new object
            Button buttonAdd = new Button();
            buttonAdd.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            buttonAdd.Location = new Point(panel1.Width - buttonAdd.Width - 105, 500);
            buttonAdd.Size = new Size(150, 50);
            buttonAdd.Text = "Adaugare";

            // Label for error message
            Label labelError = new Label();
            labelError.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            labelError.Location = new Point(panel1.Width - labelError.Width - 80, 550);
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
                    !int.TryParse(salarBazaStr, out salarBaza) || !int.TryParse(sporStr, out spor))
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
            panel1.Controls.Add(labelError);
            panel1.Controls.Add(buttonAdd);
            panel1.Controls.Add(labelNume);
            panel1.Controls.Add(labelPrenume);
            panel1.Controls.Add(labelFunctie);
            panel1.Controls.Add(labelSalarBaza);
            panel1.Controls.Add(labelSpor);
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
