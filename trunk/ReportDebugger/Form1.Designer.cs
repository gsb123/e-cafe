﻿namespace ReportDebugger
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.partnerButton1 = new GUI.PartnerButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Blokk";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OsszesitoReport";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(64, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 212);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // partnerButton1
            // 
            this.partnerButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.partnerButton1.BackColor = System.Drawing.Color.Transparent;
            this.partnerButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("partnerButton1.BackgroundImage")));
            this.partnerButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.partnerButton1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.partnerButton1.FlatAppearance.BorderSize = 2;
            this.partnerButton1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.partnerButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.partnerButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.partnerButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.partnerButton1.fPARTNER = null;
            this.partnerButton1.Location = new System.Drawing.Point(115, 108);
            this.partnerButton1.Name = "partnerButton1";
            this.partnerButton1.Size = new System.Drawing.Size(150, 50);
            this.partnerButton1.TabIndex = 4;
            this.partnerButton1.TabStop = true;
            this.partnerButton1.Text = "partnerButton1";
            this.partnerButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.partnerButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private GUI.PartnerButton partnerButton1;
    }
}

