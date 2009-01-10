﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessLogic;
using GUI;


namespace e_Cafe
{
    public partial class MMenu : Form
    {
        string ConSTR = @"server=ERNIE-HOME\SQLEXPRESS;database=ECAFE;uid=sa;password=x";
        clFIELDINFO_LIST FieldInfo;
        public static TBLObj blObj;
        Asztalok a;
        public  bool _Rendel;
        MainMenuBtn tlpButtons;

        public string DebugMessage
        {
            set { label1.Text = value; }
        }


        public MMenu()
        {
            InitializeComponent();
            FieldInfo = new clFIELDINFO_LIST(ConSTR);
            _Rendel = false;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            goMainMenu(0);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = Convert.ToString(Convert.ToDateTime(DateTime.Now).Hour) + ":" + Convert.ToString(Convert.ToDateTime(DateTime.Now).Minute) + ":" + Convert.ToString(Convert.ToDateTime(DateTime.Now).Second);
        }

        private void MMenu_Load(object sender, EventArgs e)
        {
            blObj = new TBLObj(-1, ConSTR, FieldInfo);
            initHelyek();
            Invalidate();

        }

        private void initHelyek()
        {
            Helyek cl = new Helyek(blObj);

            tlpButtons = new MainMenuBtn();
            //TableLayoutPanel tlpButtons = new TableLayoutPanel();
            tlpButtons.Location = new Point(panel8.Width, panel6.Height+panel7.Height);
            tlpButtons.Height = panel8.Height; ;
            tlpButtons.Width = 150;
            pnlHelyek.Controls.Add(tlpButtons);
            //this.Controls.Add(tlpButtons);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            tlpButtons.BringToFront();


            tlpButtons.RowCount = cl.lHelyek.Count + 1;
            
            for (int i = 0; i < (cl.lHelyek.Count); i++)
            {

                HelyButton bt = new HelyButton(cl.lHelyek[i]);
                
                bt.Location = new Point(0, 0);
                bt.Text = cl.lHelyek[i].fHELY_NEV;
                bt.TextAlign = ContentAlignment.BottomLeft;
                bt.Dock = DockStyle.Fill;
                bt.ImageList = btmImgList;
                bt.ImageIndex = 0;
                bt.ImageAlign = ContentAlignment.MiddleCenter;
                bt.FlatStyle = FlatStyle.Flat;
                bt.FlatAppearance.BorderSize = 0;
                //bt.BackColor = Color.Red;
                //bt.BackColor = Color.Transparent;
                bt.Click += HelyMenuClick;

                

                tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Absolute, 60));
                tlpButtons.Controls.Add(bt);
            }
            tlpButtons.Refresh();
            //bb.Refresh();
        }

        private void HelyMenuClick(object sender, EventArgs e)
        {
            goMainMenu(((HelyButton)sender)._Hely.fHELY_ID);
            
                //this.Controls.SetChildIndex(tlpButtons, 0);
            tlpButtons.Invalidate();
            
        }


        public void Asztal_click(object sender, EventArgs e)
        {
            if (rbRendel.Checked)
            {
                // ha egy asztalra klikkelünk

                Asztal_Button tmp_a = (Asztal_Button)sender;

                a.aList.SelectAsztal(tmp_a.Asztal_id);
                tmp_a.vSelected = true;

                DebugMessage = tmp_a.Asztal_id.ToString() + tmp_a.vSelected.ToString();

                MRendeles mr = new MRendeles(a.aList.GetItem(tmp_a.Asztal_id), blObj);
                mr.ShowDialog();
                a.RefreshAsztalok(false);

                // Választó lista megjelenítése
                /*
                                if (!a.aList.isUsed(tmp_a.Asztal_id))
                                {

                    
                                    // ha szükség van előválaztóra!!
                                    TableActionSelect preAsk = new TableActionSelect("Asztal " + a.aList.GetItem(tmp_a.Asztal_id).fASZTAL_SZAM.ToString() + ": Válasszon feladatok!");
                                    preAsk.ShowDialog();
                                    if (preAsk.DialogResult == DialogResult.OK)
                                    {
                                        if (preAsk.pChoice == "R")
                                        {
                                            panel3.Controls.Clear();
                                            CikkValaszt cv = new CikkValaszt(panel3, blObj);
                                            panel3.Controls.Add(cv);
                                            cv.Dock = DockStyle.Fill;
                                            cv.InitCikkValaszt();
                            
                                        }
                                    }
                    


                                }
                                else 
                                { // A rendelések panel aktivizálása
                                    nr1.rtHeader.Text = tmp_a.Asztal_id.ToString() + ". asztal rendelései:";


                                }
                */
            }
            tlpButtons.BringToFront();
            tlpButtons.Invalidate();

        }
        public void goMainMenu(int aHelyId)
        {
            panel3.Controls.Clear();
            a = new Asztalok(panel3, blObj, aHelyId);
            a.RefreshAsztalok(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminTools adm = new AdminTools(blObj);
            adm.ShowDialog();// = true;

            //adm.Show();
        }

        public void DoAsztalSelect(int iASZTAL_ID)
        {

        }

        private void notepad_Rendeles1_Load(object sender, EventArgs e)
        {

        }



        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {
            tlpButtons.Invalidate();
        }






    }
}