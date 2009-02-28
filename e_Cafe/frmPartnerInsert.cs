﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GUI.billentyu;
using BusinessLogic;


namespace e_Cafe
{
    public partial class frmPartnerInsert : Form
    {
        public frmPartnerInsert(int pId)
        {
            InitializeComponent();
            if (pId == -1)
            {
                vevoBindingSource.Add(new Vevo());
            }
            else
            {
                vevoBindingSource.Add(new Vevo(pId,new SqlConnection(DEFS.ConSTR)));
            }

            dynComboBindingSource.Add(new DynCombo("Vevő", "V"));
            dynComboBindingSource.Add(new DynCombo("Törzsvendég", "T"));
            dynComboBindingSource.Add(new DynCombo("Dolgozó", "D"));

            NemSource.Add(new DynCombo("Férfi", "F"));
            NemSource.Add(new DynCombo("Nő", "N"));
            //keyb21.txtRet.Visible = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = true;
            bool ok2;
            foreach (var k in vevoBindingSource.List)
            {
                
                ((Vevo)k).Save(out ok2);
                ok = ok && ok2;
            }

            if (ok) { Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_to_keyboard_Click(object sender, EventArgs e)
        {
            keyb21.txtRet.Text = ((TextBox)sender).Text;
            keyb21.OutTxtBox = (TextBox)sender;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}