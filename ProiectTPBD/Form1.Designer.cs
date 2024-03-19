namespace ProiectTPBD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            aJUTORToolStripMenuItem = new ToolStripMenuItem();
            iNTRODUCEREDATEToolStripMenuItem = new ToolStripMenuItem();
            actualizareDateToolStripMenuItem = new ToolStripMenuItem();
            adaugareAngajatiToolStripMenuItem = new ToolStripMenuItem();
            stergereAngajatiToolStripMenuItem = new ToolStripMenuItem();
            tIPARIREToolStripMenuItem = new ToolStripMenuItem();
            statPlataToolStripMenuItem = new ToolStripMenuItem();
            fluturasiToolStripMenuItem = new ToolStripMenuItem();
            mODIFPROCENTEToolStripMenuItem = new ToolStripMenuItem();
            iESIREToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10.5F);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { aJUTORToolStripMenuItem, iNTRODUCEREDATEToolStripMenuItem, tIPARIREToolStripMenuItem, mODIFPROCENTEToolStripMenuItem, iESIREToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.ShowItemToolTips = true;
            menuStrip1.Size = new Size(1467, 38);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // aJUTORToolStripMenuItem
            // 
            aJUTORToolStripMenuItem.Name = "aJUTORToolStripMenuItem";
            aJUTORToolStripMenuItem.Size = new Size(105, 34);
            aJUTORToolStripMenuItem.Text = "AJUTOR";
            aJUTORToolStripMenuItem.ToolTipText = "Informatii ajutatoare privind modul de operare in program";
            // 
            // iNTRODUCEREDATEToolStripMenuItem
            // 
            iNTRODUCEREDATEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { actualizareDateToolStripMenuItem, adaugareAngajatiToolStripMenuItem, stergereAngajatiToolStripMenuItem });
            iNTRODUCEREDATEToolStripMenuItem.Name = "iNTRODUCEREDATEToolStripMenuItem";
            iNTRODUCEREDATEToolStripMenuItem.Size = new Size(222, 34);
            iNTRODUCEREDATEToolStripMenuItem.Text = "INTRODUCERE DATE";
            // 
            // actualizareDateToolStripMenuItem
            // 
            actualizareDateToolStripMenuItem.Name = "actualizareDateToolStripMenuItem";
            actualizareDateToolStripMenuItem.Size = new Size(286, 38);
            actualizareDateToolStripMenuItem.Text = "Actualizare date";
            actualizareDateToolStripMenuItem.ToolTipText = "Actualizarea informatiilor precum: Nume, Salar de baza, Premii Brute, Retineri";
            // 
            // adaugareAngajatiToolStripMenuItem
            // 
            adaugareAngajatiToolStripMenuItem.Name = "adaugareAngajatiToolStripMenuItem";
            adaugareAngajatiToolStripMenuItem.Size = new Size(286, 38);
            adaugareAngajatiToolStripMenuItem.Text = "Adaugare angajati";
            adaugareAngajatiToolStripMenuItem.ToolTipText = "Permite adaugarea unui angajat";
            // 
            // stergereAngajatiToolStripMenuItem
            // 
            stergereAngajatiToolStripMenuItem.Name = "stergereAngajatiToolStripMenuItem";
            stergereAngajatiToolStripMenuItem.Size = new Size(286, 38);
            stergereAngajatiToolStripMenuItem.Text = "Stergere angajati";
            stergereAngajatiToolStripMenuItem.ToolTipText = "Permite stergerea unui angajat";
            // 
            // tIPARIREToolStripMenuItem
            // 
            tIPARIREToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { statPlataToolStripMenuItem, fluturasiToolStripMenuItem });
            tIPARIREToolStripMenuItem.Name = "tIPARIREToolStripMenuItem";
            tIPARIREToolStripMenuItem.Size = new Size(114, 34);
            tIPARIREToolStripMenuItem.Text = "TIPARIRE";
            // 
            // statPlataToolStripMenuItem
            // 
            statPlataToolStripMenuItem.Name = "statPlataToolStripMenuItem";
            statPlataToolStripMenuItem.Size = new Size(203, 38);
            statPlataToolStripMenuItem.Text = "Stat plata";
            statPlataToolStripMenuItem.ToolTipText = "Afiseaza pe ecran statutul de plata";
            // 
            // fluturasiToolStripMenuItem
            // 
            fluturasiToolStripMenuItem.Name = "fluturasiToolStripMenuItem";
            fluturasiToolStripMenuItem.Size = new Size(203, 38);
            fluturasiToolStripMenuItem.Text = "Fluturasi";
            fluturasiToolStripMenuItem.ToolTipText = "Afiseaza pe ecran fluturasii";
            // 
            // mODIFPROCENTEToolStripMenuItem
            // 
            mODIFPROCENTEToolStripMenuItem.Name = "mODIFPROCENTEToolStripMenuItem";
            mODIFPROCENTEToolStripMenuItem.Size = new Size(207, 34);
            mODIFPROCENTEToolStripMenuItem.Text = "MODIF_PROCENTE";
            mODIFPROCENTEToolStripMenuItem.ToolTipText = "Permite modificarea impozitului";
            // 
            // iESIREToolStripMenuItem
            // 
            iESIREToolStripMenuItem.Name = "iESIREToolStripMenuItem";
            iESIREToolStripMenuItem.Size = new Size(87, 34);
            iESIREToolStripMenuItem.Text = "IESIRE";
            iESIREToolStripMenuItem.ToolTipText = "Iesire din program";
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(1443, 627);
            panel1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1467, 696);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem aJUTORToolStripMenuItem;
        private ToolStripMenuItem iNTRODUCEREDATEToolStripMenuItem;
        private ToolStripMenuItem actualizareDateToolStripMenuItem;
        private ToolStripMenuItem adaugareAngajatiToolStripMenuItem;
        private ToolStripMenuItem stergereAngajatiToolStripMenuItem;
        private ToolStripMenuItem tIPARIREToolStripMenuItem;
        private ToolStripMenuItem statPlataToolStripMenuItem;
        private ToolStripMenuItem fluturasiToolStripMenuItem;
        private ToolStripMenuItem mODIFPROCENTEToolStripMenuItem;
        private ToolStripMenuItem iESIREToolStripMenuItem;
        private Panel panel1;
    }
}
