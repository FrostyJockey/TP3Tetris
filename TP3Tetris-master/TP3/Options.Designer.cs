namespace TP3
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarLignes = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarColonnes = new System.Windows.Forms.TrackBar();
            this.checkBoxSon = new System.Windows.Forms.CheckBox();
            this.buttonConfirmer = new System.Windows.Forms.Button();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.checkBoxMusique = new System.Windows.Forms.CheckBox();
            this.textBoxColonnes = new System.Windows.Forms.TextBox();
            this.textBoxLignes = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLignes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarColonnes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.trackBarLignes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBarColonnes, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 93);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // trackBarLignes
            // 
            this.trackBarLignes.Location = new System.Drawing.Point(205, 49);
            this.trackBarLignes.Maximum = 30;
            this.trackBarLignes.Minimum = 10;
            this.trackBarLignes.Name = "trackBarLignes";
            this.trackBarLignes.Size = new System.Drawing.Size(197, 41);
            this.trackBarLignes.TabIndex = 3;
            this.trackBarLignes.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarLignes.Value = 20;
            this.trackBarLignes.Scroll += new System.EventHandler(this.trackBarLignes_Scroll);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 47);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre de lignes:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de colonnes:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarColonnes
            // 
            this.trackBarColonnes.Location = new System.Drawing.Point(205, 3);
            this.trackBarColonnes.Maximum = 20;
            this.trackBarColonnes.Minimum = 5;
            this.trackBarColonnes.Name = "trackBarColonnes";
            this.trackBarColonnes.Size = new System.Drawing.Size(197, 40);
            this.trackBarColonnes.TabIndex = 2;
            this.trackBarColonnes.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarColonnes.Value = 10;
            this.trackBarColonnes.Scroll += new System.EventHandler(this.trackBarColonnes_Scroll);
            // 
            // checkBoxSon
            // 
            this.checkBoxSon.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSon.Location = new System.Drawing.Point(13, 112);
            this.checkBoxSon.Name = "checkBoxSon";
            this.checkBoxSon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxSon.Size = new System.Drawing.Size(151, 32);
            this.checkBoxSon.TabIndex = 1;
            this.checkBoxSon.Text = "?Arrêter le son";
            this.checkBoxSon.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.checkBoxSon.UseVisualStyleBackColor = true;
            this.checkBoxSon.CheckedChanged += new System.EventHandler(this.checkBoxSon_CheckedChanged);
            // 
            // buttonConfirmer
            // 
            this.buttonConfirmer.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConfirmer.Location = new System.Drawing.Point(314, 149);
            this.buttonConfirmer.Name = "buttonConfirmer";
            this.buttonConfirmer.Size = new System.Drawing.Size(87, 32);
            this.buttonConfirmer.TabIndex = 2;
            this.buttonConfirmer.Text = "Confirmer";
            this.buttonConfirmer.UseVisualStyleBackColor = true;
            this.buttonConfirmer.Click += new System.EventHandler(this.buttonConfirmer_Click);
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAnnuler.Location = new System.Drawing.Point(425, 149);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(87, 32);
            this.buttonAnnuler.TabIndex = 3;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = true;
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // checkBoxMusique
            // 
            this.checkBoxMusique.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMusique.Location = new System.Drawing.Point(13, 150);
            this.checkBoxMusique.Name = "checkBoxMusique";
            this.checkBoxMusique.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxMusique.Size = new System.Drawing.Size(189, 32);
            this.checkBoxMusique.TabIndex = 4;
            this.checkBoxMusique.Text = "?Arrêter la musique";
            this.checkBoxMusique.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.checkBoxMusique.UseVisualStyleBackColor = true;
            this.checkBoxMusique.CheckedChanged += new System.EventHandler(this.checkBoxMusique_CheckedChanged);
            // 
            // textBoxColonnes
            // 
            this.textBoxColonnes.Enabled = false;
            this.textBoxColonnes.Location = new System.Drawing.Point(425, 30);
            this.textBoxColonnes.Name = "textBoxColonnes";
            this.textBoxColonnes.Size = new System.Drawing.Size(87, 20);
            this.textBoxColonnes.TabIndex = 5;
            this.textBoxColonnes.Text = "10";
            // 
            // textBoxLignes
            // 
            this.textBoxLignes.Enabled = false;
            this.textBoxLignes.Location = new System.Drawing.Point(425, 76);
            this.textBoxLignes.Name = "textBoxLignes";
            this.textBoxLignes.Size = new System.Drawing.Size(87, 20);
            this.textBoxLignes.TabIndex = 6;
            this.textBoxLignes.Text = "20";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 193);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxLignes);
            this.Controls.Add(this.textBoxColonnes);
            this.Controls.Add(this.checkBoxMusique);
            this.Controls.Add(this.buttonAnnuler);
            this.Controls.Add(this.buttonConfirmer);
            this.Controls.Add(this.checkBoxSon);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Options";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLignes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarColonnes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxSon;
        private System.Windows.Forms.Button buttonConfirmer;
        private System.Windows.Forms.Button buttonAnnuler;
        private System.Windows.Forms.TrackBar trackBarLignes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarColonnes;
        private System.Windows.Forms.CheckBox checkBoxMusique;
        private System.Windows.Forms.TextBox textBoxColonnes;
        private System.Windows.Forms.TextBox textBoxLignes;
    }
}