﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;

namespace e_Cafe.Keszlet
{
    public partial class frmSelejtez : Form
    {
        private Cikk atvCikk;
        private int _defRaktar;
        private int _defCikk;


        public frmSelejtez()
        {
            InitializeComponent();
            // TODO: This line of code loads data into the 'eCAFEDataSetRAKTAR.RAKTAR' table. You can move, or remove it, as needed.
            this.rAKTARTableAdapter.Fill(this.eCAFEDataSetRAKTAR.RAKTAR);
            lblCikkNev.Text = "";
            lblKeszlet.Text = "";
            lblME.Text = "";
        }

        public frmSelejtez(int cikk, int rakt)
        {
            InitializeComponent();

            _defCikk = cikk;
            _defRaktar = rakt;
             atvCikk =  CikkSelector.SelectCikk(cikk);
             atvCikk.getKeszlet();
             SetLabels();
             // TODO: This line of code loads data into the 'eCAFEDataSetRAKTAR.RAKTAR' table. You can move, or remove it, as needed.
             this.rAKTARTableAdapter.Fill(this.eCAFEDataSetRAKTAR.RAKTAR);
             cikkKeszletBindingSource.Clear();
             foreach (var k in atvCikk.lKESZLET)
             {
                 cikkKeszletBindingSource.Add(k);
             }
        }

        private void SetLabels()
        {
            lblCikkNev.Text = atvCikk.MEGNEVEZES;
            lblME.Text = atvCikk.MEGYS_MEGNEVEZES;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void cikkKeszletBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (cikkKeszletBindingSource.Current != null)
            {
                lblKeszlet.Text = ((CikkKeszlet)cikkKeszletBindingSource.Current).fKESZLET.ToString() + " " + atvCikk.MEGYS_MEGNEVEZES;


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CikkSelector cs = new CikkSelector();
            cs.ShowDialog();
            if (cs.DialogResult == DialogResult.OK)
            {
                atvCikk = cs.c;
                atvCikk.getKeszlet();
                SetLabels();
                cikkKeszletBindingSource.Clear();
                foreach (var k in atvCikk.lKESZLET)
                {
                    cikkKeszletBindingSource.Add(k);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            bool ok = true;

            double menny = 0;
            int from_rakt = -1;

            if (atvCikk == null) { ok = false; }

            try
            {
                menny = Convert.ToDouble(txtMenny.Text);
            }
            catch (Exception x)
            {
                ok = false;
                DEFS.DebugLog(x.StackTrace);
            }

            if (menny == 0) { ok = false; }

            if (cikkKeszletBindingSource.Current != null)
            {
                from_rakt = ((CikkKeszlet)cikkKeszletBindingSource.Current).fRAKTAR_ID;
            }
            if (!(from_rakt > 0)) { ok = false; }



            if (ok)
            {
                SqlConnection c = new SqlConnection(DEFS.ConSTR);
                SqlCommand cmdKeszlAtvezet = new SqlCommand("SP_KESZLET_SELEJT", c);

                cmdKeszlAtvezet.CommandType = System.Data.CommandType.StoredProcedure;

                cmdKeszlAtvezet.Parameters.Add("@from_raktar", SqlDbType.Int);
                cmdKeszlAtvezet.Parameters["@from_raktar"].Direction = ParameterDirection.Input;
                cmdKeszlAtvezet.Parameters["@from_raktar"].Value = from_rakt;
                cmdKeszlAtvezet.Parameters.Add("@cikk_id", SqlDbType.Int);
                cmdKeszlAtvezet.Parameters["@cikk_id"].Direction = ParameterDirection.Input;
                cmdKeszlAtvezet.Parameters["@cikk_id"].Value = atvCikk.CIKK_ID;
                cmdKeszlAtvezet.Parameters.Add("@menny", SqlDbType.Float);
                cmdKeszlAtvezet.Parameters["@menny"].Direction = ParameterDirection.Input;
                cmdKeszlAtvezet.Parameters["@menny"].Value = menny;
                c.Open();
                cmdKeszlAtvezet.ExecuteNonQuery();


                c.Close();

            }

            DEFS.SendShortMessage("Sejeltezés sikeres!",900);
        
        }
    }
}