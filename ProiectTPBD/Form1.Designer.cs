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
            calculSalariiToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { aJUTORToolStripMenuItem, iNTRODUCEREDATEToolStripMenuItem, tIPARIREToolStripMenuItem, mODIFPROCENTEToolStripMenuItem, iESIREToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1467, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // aJUTORToolStripMenuItem
            // 
            aJUTORToolStripMenuItem.Name = "aJUTORToolStripMenuItem";
            aJUTORToolStripMenuItem.Size = new Size(92, 29);
            aJUTORToolStripMenuItem.Text = "AJUTOR";
            // 
            // iNTRODUCEREDATEToolStripMenuItem
            // 
            iNTRODUCEREDATEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { actualizareDateToolStripMenuItem, adaugareAngajatiToolStripMenuItem, stergereAngajatiToolStripMenuItem, calculSalariiToolStripMenuItem });
            iNTRODUCEREDATEToolStripMenuItem.Name = "iNTRODUCEREDATEToolStripMenuItem";
            iNTRODUCEREDATEToolStripMenuItem.Size = new Size(192, 29);
            iNTRODUCEREDATEToolStripMenuItem.Text = "INTRODUCERE DATE";
            // 
            // actualizareDateToolStripMenuItem
            // 
            actualizareDateToolStripMenuItem.Name = "actualizareDateToolStripMenuItem";
            actualizareDateToolStripMenuItem.Size = new Size(270, 34);
            actualizareDateToolStripMenuItem.Text = "Actualizare date";
            // 
            // adaugareAngajatiToolStripMenuItem
            // 
            adaugareAngajatiToolStripMenuItem.Name = "adaugareAngajatiToolStripMenuItem";
            adaugareAngajatiToolStripMenuItem.Size = new Size(270, 34);
            adaugareAngajatiToolStripMenuItem.Text = "Adaugare angajati";
            // 
            // stergereAngajatiToolStripMenuItem
            // 
            stergereAngajatiToolStripMenuItem.Name = "stergereAngajatiToolStripMenuItem";
            stergereAngajatiToolStripMenuItem.Size = new Size(270, 34);
            stergereAngajatiToolStripMenuItem.Text = "Stergere angajati";
            // 
            // calculSalariiToolStripMenuItem
            // 
            calculSalariiToolStripMenuItem.Name = "calculSalariiToolStripMenuItem";
            calculSalariiToolStripMenuItem.Size = new Size(270, 34);
            calculSalariiToolStripMenuItem.Text = "Calcul salarii";
            // 
            // tIPARIREToolStripMenuItem
            // 
            tIPARIREToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { statPlataToolStripMenuItem, fluturasiToolStripMenuItem });
            tIPARIREToolStripMenuItem.Name = "tIPARIREToolStripMenuItem";
            tIPARIREToolStripMenuItem.Size = new Size(99, 29);
            tIPARIREToolStripMenuItem.Text = "TIPARIRE";
            // 
            // statPlataToolStripMenuItem
            // 
            statPlataToolStripMenuItem.Name = "statPlataToolStripMenuItem";
            statPlataToolStripMenuItem.Size = new Size(188, 34);
            statPlataToolStripMenuItem.Text = "Stat plata";
            // 
            // fluturasiToolStripMenuItem
            // 
            fluturasiToolStripMenuItem.Name = "fluturasiToolStripMenuItem";
            fluturasiToolStripMenuItem.Size = new Size(188, 34);
            fluturasiToolStripMenuItem.Text = "Fluturasi";
            // 
            // mODIFPROCENTEToolStripMenuItem
            // 
            mODIFPROCENTEToolStripMenuItem.Name = "mODIFPROCENTEToolStripMenuItem";
            mODIFPROCENTEToolStripMenuItem.Size = new Size(178, 29);
            mODIFPROCENTEToolStripMenuItem.Text = "MODIF_PROCENTE";
            // 
            // iESIREToolStripMenuItem
            // 
            iESIREToolStripMenuItem.Name = "iESIREToolStripMenuItem";
            iESIREToolStripMenuItem.Size = new Size(77, 29);
            iESIREToolStripMenuItem.Text = "IESIRE";
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(1377, 586);
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
        private ToolStripMenuItem calculSalariiToolStripMenuItem;
        private ToolStripMenuItem tIPARIREToolStripMenuItem;
        private ToolStripMenuItem statPlataToolStripMenuItem;
        private ToolStripMenuItem fluturasiToolStripMenuItem;
        private ToolStripMenuItem mODIFPROCENTEToolStripMenuItem;
        private ToolStripMenuItem iESIREToolStripMenuItem;
        private Panel panel1;
    }
}
