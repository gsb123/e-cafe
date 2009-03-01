﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NSpring.Logging;
using NSpring.Logging.EventFormatters;


namespace BusinessLogic
{
    
    public abstract class DEFS
    {
        public static string ConSTR = "X";//@"server=ERNIE-HOME\SQLEXPRESS;database=ECAFE;uid=sa;password=x";
        public static Color Asztal_hatter = Color.FromArgb(102, 102, 102); //System.Drawing.Color.Gray;
        public static Color Selected_Color = Color.FromArgb(102, 102, 102); //System.Drawing.Color.DarkGray; 
        public static Font f2 = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

        public static int NyitNap_EV;
        public static int NyitNap_HO;
        public static int NyitNap_NAP;

        private static Logger APP_LOG;

        public static _User LogInUser;


        public static void LoadNyitottNap()
        {
            SqlConnection c = new SqlConnection(ConSTR);
            SqlCommand cmdNyitNap = new SqlCommand("getNyitottNap", c);
            cmdNyitNap.CommandType = System.Data.CommandType.StoredProcedure;

            cmdNyitNap.Parameters.Add("@EV", SqlDbType.Int);
            cmdNyitNap.Parameters["@EV"].Direction = ParameterDirection.Output;
            cmdNyitNap.Parameters.Add("@HO", SqlDbType.Int);
            cmdNyitNap.Parameters["@HO"].Direction = ParameterDirection.Output;
            cmdNyitNap.Parameters.Add("@NAP", SqlDbType.Int);
            cmdNyitNap.Parameters["@NAP"].Direction = ParameterDirection.Output;

            c.Open();
            cmdNyitNap.ExecuteNonQuery();
            NyitNap_EV = (int)cmdNyitNap.Parameters["@EV"].Value;
            NyitNap_HO = (int)cmdNyitNap.Parameters["@HO"].Value;
            NyitNap_NAP = (int)cmdNyitNap.Parameters["@NAP"].Value;
            c.Close();

            
        }

        public static int GetDBVER()
        {
            int tmpret = 0;
            SqlConnection c = new SqlConnection(ConSTR);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = c;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT Cast(DB_VER as integer) as DB_VER FROM VERSION";
            c.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                tmpret = (int)rdr["DB_VER"];
            }

            return tmpret;
        }

        public static void createLogger()
        {
            APP_LOG = Logger.CreateFileLogger(AppDomain.CurrentDomain.BaseDirectory + "\\log\\e_cafe_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + "_" +
                                                    DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second +".log");
            APP_LOG.IsBufferingEnabled = true;
            APP_LOG.BufferSize = 1000;
            APP_LOG.EventFormatter = new PatternEventFormatter("{mm}/{dd}/{yyyy} {time}  [{ln:uc}]  {msg}");
            APP_LOG.Open();
        }


        public static void ExLog( string t)
        {
            if (!APP_LOG.IsOpen) { APP_LOG.Open(); }
            APP_LOG.Log("\n");
            APP_LOG.Log(Level.Exception, t);
            APP_LOG.Log("\n");


            APP_LOG.Close();
        }

        public static void DebugLog(string t)
        {
            if (!APP_LOG.IsOpen) { APP_LOG.Open(); }
            APP_LOG.Log("\n");
            APP_LOG.Log(Level.Debug, t);
            APP_LOG.Log("\n");


            APP_LOG.Close();
        }

        public static void ObjLog(string t, object o)
        {
            if (!APP_LOG.IsOpen) { APP_LOG.Open(); }
            APP_LOG.Log("\n");
            APP_LOG.Log(t, o.ToString());
            APP_LOG.Log("\n");


            APP_LOG.Close();
        }


        public static void log(Level l, string t)
        {
            if (!APP_LOG.IsOpen) {APP_LOG.Open();}
            APP_LOG.Log("\n");
            APP_LOG.Log(l, t);
            APP_LOG.Log("\n");

            APP_LOG.Close();
        }

        public static void SendSaveErrMessage(string t)
        {
            string s = "Hiba a mentés során!" + "\n" + "Oka:" +"\n";
            log(Level.Exception, s + t);
            MessageBox.Show(s + t);

        }

        public static void SendValidatingMessage(string hol, string t)
        {
            string s = "Érvénytelen adatok a "+hol+ " mezőben !" + "\n" + "Oka:" + "\n";
            log(Level.Exception, s + t);
            MessageBox.Show(s + t);

        }

        public static void SendInfoMessage(string t)
        {
            
            log(Level.Info, t);
            MessageBox.Show(t);

        }
    }

    public enum Fizmond : int
    {
        Keszpenz = 1,
        Bankkartya = 2,
        Utalvany = 3,
        egyeb = 4
    }

    public class ertTip
    {
        private string _KOD;
        private string _NEV;

        public string KOD
        {
            get { return (_KOD); }
            set { _KOD = value; }
        }

        public string NEV
        {
            get { return (_NEV); }
            set { _NEV = value; }
        }
        public ertTip(string k, string n)
        {
            _KOD = k;
            _NEV = n;
        }
    }

    public class DynCombo
    {

        #region DISPLAY_MEMBER
        private string _disp;
        public string DISPLAY_MEMBER
        {
            get { return (_disp); }
            set { _disp = value; }
        }
        #endregion

        #region VALUE_MEMBER
        private string _value;
        public string VALUE_MEMBER
        {
            get { return (_value); }
            set { _value = value; }
        }
        #endregion
        public DynCombo(string pDisp, string pValue)
        {
            _disp = pDisp;
            _value = pValue;

        }

    }




   
}
