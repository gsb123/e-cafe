﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;

namespace e_Cafe.Admin
{
    public partial class frmNapiZaras : Form
    {
        NapZaras nz = new NapZaras();
        int maxEvents = 6;
        int processCounter = 0;
        public frmNapiZaras()
        {
            InitializeComponent();
            
        }

        private void frmNapiZaras_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (processCounter)
            {
                case 0:
                    {
                        #region nyitott nap ellenőrzése
                        if (nz.VaneNyitotNap())
                        {
                            pnlVanNyitottNap.BackgroundImage = global::GUI.Properties.Resources.OK_ICON;
                            
                        }
                        else
                        {
                            pnlVanNyitottNap.BackgroundImage = global::GUI.Properties.Resources.ERROR_ICON;

                        }
                        processCounter++;
                        pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        break;
                        #endregion

                    }
                case 1:
                    {
                        #region Aktuális nyitot nap
                        lblHO.Text = DEFS.NyitNap_HO.ToString();
                        lblNAP.Text = DEFS.NyitNap_NAP.ToString();
                        lblNEV.Text = DEFS.NyitNap_EV.ToString();
                        processCounter++;
                        pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        break;
                        #endregion

                    }
                case 2:
                    {
                        #region nyitott rendelések ellenőrzése
                        if (false)
                        {
                            pnlNyitRendel.BackgroundImage = global::GUI.Properties.Resources.OK_ICON;
                        }
                        else
                        {
                            pnlNyitRendel.BackgroundImage = global::GUI.Properties.Resources.ERROR_ICON;
                        }
                        processCounter++;
                        pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        break;
                        #endregion

                    }
                case 3:
                    {
                        #region tartozások ellenőrzése
                        if (false)
                        {
                            pnlNyitTartozas.BackgroundImage = global::GUI.Properties.Resources.OK_ICON;
                        }
                        else
                        {
                            pnlNyitTartozas.BackgroundImage = global::GUI.Properties.Resources.ERROR_ICON;
                        }
                        processCounter++;
                        pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        break;
                        #endregion

                    }
                case 4:
                    {
                        #region negatív raktárkészlet
                        if (false)
                        {
                            pnlNegRaktar.BackgroundImage = global::GUI.Properties.Resources.OK_ICON;
                        }
                        else
                        {
                            pnlNegRaktar.BackgroundImage = global::GUI.Properties.Resources.ERROR_ICON;
                        }
                        processCounter++;
                        pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        break;
                        #endregion

                    }
                case 5:
                    {
                        #region adatbázis konzisztencia
                        if (false)
                        {
                            pnlKonzisztencia.BackgroundImage = global::GUI.Properties.Resources.OK_ICON;
                        }
                        else
                        {
                            pnlKonzisztencia.BackgroundImage = global::GUI.Properties.Resources.ERROR_ICON;
                        }
                        processCounter++;
                        if ((pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0))) > 100)
                        {
                            pbStatus.Value = 100;
                        }
                        else
                        {
                            pbStatus.Value = pbStatus.Value + Convert.ToInt16(Math.Round((100.0 / maxEvents), 0));
                        }
                        break;
                        #endregion

                    }
                default:
                    {
                        timer1.Enabled = false;
                        break;

                    }
            }


        }
    }
}