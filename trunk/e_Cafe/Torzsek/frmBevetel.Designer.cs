﻿namespace e_Cafe
{
    partial class frmBevetel
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSzallitolevel = new System.Windows.Forms.TextBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblSzallito = new System.Windows.Forms.Label();
            this.txtSzamlaszam = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.sORIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bEVETELFEJIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIKKIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mENNYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bESZARDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nETTOERTEKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFAERTEKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bRUTTOERTEKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rAKTARIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fELADVADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mEGJEGYZESDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bevetelSorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bevetelSorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Szállítólevél";
            // 
            // txtSzallitolevel
            // 
            this.txtSzallitolevel.Location = new System.Drawing.Point(82, 6);
            this.txtSzallitolevel.Name = "txtSzallitolevel";
            this.txtSzallitolevel.Size = new System.Drawing.Size(100, 20);
            this.txtSzallitolevel.TabIndex = 1;
            // 
            // dtpDatum
            // 
            this.dtpDatum.Location = new System.Drawing.Point(493, 5);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(120, 20);
            this.dtpDatum.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(423, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dátum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Szállító";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Választ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblSzallito
            // 
            this.lblSzallito.AutoSize = true;
            this.lblSzallito.Location = new System.Drawing.Point(167, 37);
            this.lblSzallito.Name = "lblSzallito";
            this.lblSzallito.Size = new System.Drawing.Size(35, 13);
            this.lblSzallito.TabIndex = 5;
            this.lblSzallito.Text = "label4";
            // 
            // txtSzamlaszam
            // 
            this.txtSzamlaszam.Location = new System.Drawing.Point(288, 6);
            this.txtSzamlaszam.Name = "txtSzamlaszam";
            this.txtSzamlaszam.Size = new System.Drawing.Size(100, 20);
            this.txtSzamlaszam.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Számlaszám";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sORIDDataGridViewTextBoxColumn,
            this.bEVETELFEJIDDataGridViewTextBoxColumn,
            this.cIKKIDDataGridViewTextBoxColumn,
            this.mENNYDataGridViewTextBoxColumn,
            this.bESZARDataGridViewTextBoxColumn,
            this.nETTOERTEKDataGridViewTextBoxColumn,
            this.aFAERTEKDataGridViewTextBoxColumn,
            this.bRUTTOERTEKDataGridViewTextBoxColumn,
            this.rAKTARIDDataGridViewTextBoxColumn,
            this.fELADVADataGridViewTextBoxColumn,
            this.mEGJEGYZESDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bevetelSorBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(647, 143);
            this.dataGridView1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "új sor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sORIDDataGridViewTextBoxColumn
            // 
            this.sORIDDataGridViewTextBoxColumn.DataPropertyName = "SOR_ID";
            this.sORIDDataGridViewTextBoxColumn.HeaderText = "SOR_ID";
            this.sORIDDataGridViewTextBoxColumn.Name = "sORIDDataGridViewTextBoxColumn";
            this.sORIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.sORIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // bEVETELFEJIDDataGridViewTextBoxColumn
            // 
            this.bEVETELFEJIDDataGridViewTextBoxColumn.DataPropertyName = "BEVETEL_FEJ_ID";
            this.bEVETELFEJIDDataGridViewTextBoxColumn.HeaderText = "BEVETEL_FEJ_ID";
            this.bEVETELFEJIDDataGridViewTextBoxColumn.Name = "bEVETELFEJIDDataGridViewTextBoxColumn";
            this.bEVETELFEJIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.bEVETELFEJIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // cIKKIDDataGridViewTextBoxColumn
            // 
            this.cIKKIDDataGridViewTextBoxColumn.DataPropertyName = "CIKK_ID";
            this.cIKKIDDataGridViewTextBoxColumn.HeaderText = "CIKK_ID";
            this.cIKKIDDataGridViewTextBoxColumn.Name = "cIKKIDDataGridViewTextBoxColumn";
            this.cIKKIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mENNYDataGridViewTextBoxColumn
            // 
            this.mENNYDataGridViewTextBoxColumn.DataPropertyName = "MENNY";
            this.mENNYDataGridViewTextBoxColumn.HeaderText = "MENNY";
            this.mENNYDataGridViewTextBoxColumn.Name = "mENNYDataGridViewTextBoxColumn";
            this.mENNYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bESZARDataGridViewTextBoxColumn
            // 
            this.bESZARDataGridViewTextBoxColumn.DataPropertyName = "BESZ_AR";
            this.bESZARDataGridViewTextBoxColumn.HeaderText = "BESZ_AR";
            this.bESZARDataGridViewTextBoxColumn.Name = "bESZARDataGridViewTextBoxColumn";
            this.bESZARDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nETTOERTEKDataGridViewTextBoxColumn
            // 
            this.nETTOERTEKDataGridViewTextBoxColumn.DataPropertyName = "NETTO_ERTEK";
            this.nETTOERTEKDataGridViewTextBoxColumn.HeaderText = "NETTO_ERTEK";
            this.nETTOERTEKDataGridViewTextBoxColumn.Name = "nETTOERTEKDataGridViewTextBoxColumn";
            this.nETTOERTEKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aFAERTEKDataGridViewTextBoxColumn
            // 
            this.aFAERTEKDataGridViewTextBoxColumn.DataPropertyName = "AFA_ERTEK";
            this.aFAERTEKDataGridViewTextBoxColumn.HeaderText = "AFA_ERTEK";
            this.aFAERTEKDataGridViewTextBoxColumn.Name = "aFAERTEKDataGridViewTextBoxColumn";
            this.aFAERTEKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bRUTTOERTEKDataGridViewTextBoxColumn
            // 
            this.bRUTTOERTEKDataGridViewTextBoxColumn.DataPropertyName = "BRUTTO_ERTEK";
            this.bRUTTOERTEKDataGridViewTextBoxColumn.HeaderText = "BRUTTO_ERTEK";
            this.bRUTTOERTEKDataGridViewTextBoxColumn.Name = "bRUTTOERTEKDataGridViewTextBoxColumn";
            this.bRUTTOERTEKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rAKTARIDDataGridViewTextBoxColumn
            // 
            this.rAKTARIDDataGridViewTextBoxColumn.DataPropertyName = "RAKTAR_ID";
            this.rAKTARIDDataGridViewTextBoxColumn.HeaderText = "RAKTAR_ID";
            this.rAKTARIDDataGridViewTextBoxColumn.Name = "rAKTARIDDataGridViewTextBoxColumn";
            this.rAKTARIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fELADVADataGridViewTextBoxColumn
            // 
            this.fELADVADataGridViewTextBoxColumn.DataPropertyName = "FELADVA";
            this.fELADVADataGridViewTextBoxColumn.HeaderText = "FELADVA";
            this.fELADVADataGridViewTextBoxColumn.Name = "fELADVADataGridViewTextBoxColumn";
            this.fELADVADataGridViewTextBoxColumn.ReadOnly = true;
            this.fELADVADataGridViewTextBoxColumn.Visible = false;
            // 
            // mEGJEGYZESDataGridViewTextBoxColumn
            // 
            this.mEGJEGYZESDataGridViewTextBoxColumn.DataPropertyName = "MEGJEGYZES";
            this.mEGJEGYZESDataGridViewTextBoxColumn.HeaderText = "MEGJEGYZES";
            this.mEGJEGYZESDataGridViewTextBoxColumn.Name = "mEGJEGYZESDataGridViewTextBoxColumn";
            this.mEGJEGYZESDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bevetelSorBindingSource
            // 
            this.bevetelSorBindingSource.DataSource = typeof(BusinessLogic.BevetelSor);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(572, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Bezár";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmBevetel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 236);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblSzallito);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSzamlaszam);
            this.Controls.Add(this.txtSzallitolevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "frmBevetel";
            this.Text = "frmBevetel";
            this.Load += new System.EventHandler(this.frmBevetel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bevetelSorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSzallitolevel;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblSzallito;
        private System.Windows.Forms.TextBox txtSzamlaszam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bevetelSorBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn sORIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bEVETELFEJIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIKKIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mENNYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bESZARDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nETTOERTEKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFAERTEKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bRUTTOERTEKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rAKTARIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fELADVADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEGJEGYZESDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}