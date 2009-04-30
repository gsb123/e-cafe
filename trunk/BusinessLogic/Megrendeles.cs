﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace BusinessLogic
{

    public class Megrendeles
    {
        #region Properties

        public bool isModified;

        private int _MEGRENDELES_FEJ_ID;
        public int MEGRENDELES_FEJ_ID
        {
            get { return (_MEGRENDELES_FEJ_ID); }
            set
            {
                if (_MEGRENDELES_FEJ_ID != value) { isModified = true; }
                _MEGRENDELES_FEJ_ID = value;
            }
        }

        private DateTime _DATUM;
         public DateTime DATUM
        {
            get { return (_DATUM); }
            set
            {
                if (_DATUM != value) { isModified = true; }
                _DATUM = value;
            }
        }

        private int _SZALLITO_ID;
        public int SZALLITO_ID
        {
            get { return (_SZALLITO_ID); }
            set { if (_SZALLITO_ID != value) { isModified = true; } _SZALLITO_ID = value; }
        }

        private string _SORSZAM;
        public string SORSZAM
        {
            get { return (_SORSZAM); }
            set { if (_SORSZAM != value) { isModified = true; } _SORSZAM = value; }
        }

        private int _LEZART;
        public bool LEZART
        {
            get { return (_LEZART == 1); }
            set
            {
                if (value) { _LEZART = 1; }
                else { _LEZART = 0; }
            }
        }

       

        #endregion

        public Megrendeles(int pSzallito)
        {

            _MEGRENDELES_FEJ_ID = -1;
            _SZALLITO_ID = pSzallito;
            _SORSZAM = "";
            _LEZART = 0;
            _DATUM = DateTime.Now;

        }


        public Megrendeles(int pM_FejId, SqlConnection c)
        {
            if (c.State == ConnectionState.Closed) { c.Open(); }
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = c;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT MEGRENDELES_FEJ_ID ,DATUM ,SZALLITO_ID ,SORSZAM ,LEZART FROM  MEGRENDELES_FEJ WHERE MEGRENDELES_FEJ_ID =" + pM_FejId.ToString();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                _MEGRENDELES_FEJ_ID = (int)rdr["MEGRENDELES_FEJ_ID"];

                if (!DBNull.Value.Equals(rdr["DATUM"])) { _DATUM = (DateTime)rdr["DATUM"]; }
                else { _DATUM = DateTime.Now; }

                if (!DBNull.Value.Equals(rdr["SZALLITO_ID"])) { _SZALLITO_ID= (int)rdr["SZALLITO_ID"]; }
                else { _SZALLITO_ID = -1; }

                if (!DBNull.Value.Equals(rdr["SORSZAM"])) { _SORSZAM= (string)rdr["SORSZAM"]; }
                else { _SORSZAM = ""; }

                if (!DBNull.Value.Equals(rdr["LEZART"])) { _LEZART = (int)rdr["LEZART"]; }
                else { _LEZART = 0; }

            }
            rdr.Close();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT SOR_ID FROM MEGRENDELES_SOR WHERE FEJ_ID =" + pM_FejId.ToString();

            

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lMegrendelesSorok.Add(new MegrendelesSor((int)rdr["SOR_ID"], new SqlConnection(DEFS.ConSTR)));

            }
            rdr.Close();


            c.Close();
            isModified = false;



        }

        public int Save()
        {
            int new_p_id = _MEGRENDELES_FEJ_ID;
            SqlConnection c = new SqlConnection(DEFS.ConSTR);
            c.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;

            

            switch (_MEGRENDELES_FEJ_ID)
            {
                case -1:
                    {
                        //új rekord!!
                        cmd.CommandText = "INSERT INTO MEGRENDELES_FEJ " +
                           " (DATUM " +
                           " ,SZALLITO_ID " +
                           " ,SORSZAM " +
                           " ,LEZART " +

                            " ) " +
                           " VALUES " +
                           " (@DATUM " +
                           " ,@SZALLITO_ID " +
                           " ,@SORSZAM " +
                           " ,@LEZART " +
                           " )  SET @newid = SCOPE_IDENTITY()";
                        cmd.Parameters.Add(new SqlParameter("newid", SqlDbType.Int));
                        cmd.Parameters["newid"].Direction = ParameterDirection.Output;
                        break;
                    }
                default:
                    {
                        
                        cmd.CommandText = "UPDATE MEGRENDELES_FEJ " +
                                          " SET DATUM = @DATUM " +
                                          "    ,SZALLITO_ID = @SZALLITO_ID " +
                                          "    ,SORSZAM = @SORSZAM " +
                                          "    ,LEZART = @LEZART " +

                                          " WHERE MEGRENDELES_FEJ_ID= @MEGRENDELES_FEJ_ID";
                        cmd.Parameters.Add(new SqlParameter("BEVETEL_FEJ", SqlDbType.Int));
                        cmd.Parameters["BEVETEL_FEJ"].Value = _MEGRENDELES_FEJ_ID;
                        break;
                    }
            }
            cmd.Parameters.Add(new SqlParameter("DATUM", SqlDbType.DateTime));

            cmd.Parameters.Add(new SqlParameter("SZALLITO_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("LEZART", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("SORSZAM", SqlDbType.VarChar));


            

            cmd.Parameters["DATUM"].Value = _DATUM;
            cmd.Parameters["SZALLITO_ID"].Value = _SZALLITO_ID;
            cmd.Parameters["LEZART"].Value = _LEZART;
            cmd.Parameters["SORSZAM"].Value = _SORSZAM;

            //try
            //{
            cmd.ExecuteNonQuery();

            if (_MEGRENDELES_FEJ_ID == -1)
            {
                new_p_id = (int)cmd.Parameters["newid"].Value;
            }
            

            _MEGRENDELES_FEJ_ID = new_p_id;
            c.Close();
            return (new_p_id);
        }

        public List<MegrendelesSor> lMegrendelesSorok = new List<MegrendelesSor>();

        public void addTetel(int pSor)
        {

            lMegrendelesSorok.Add(new MegrendelesSor(pSor, new SqlConnection(DEFS.ConSTR)));
        }

        public void addTetel(Cikk _c, int _fej)
        {
            lMegrendelesSorok.Add(new MegrendelesSor(_c, _fej));
        }

    }

    public class MegrendelesSor
    {
        #region Properties

        public bool isModified;

        private int _SOR_ID;
        public int SOR_ID
        {
            get { return (_SOR_ID); }
            set
            {
                if (_SOR_ID != value) { isModified = true; }
                _SOR_ID = value;
            }
        }
        
        private int _FEJ_ID;
        public int FEJ_ID
        {
            get { return (_FEJ_ID); }
            set
            {
                if (_FEJ_ID != value) { isModified = true; }
                _FEJ_ID = value;
            }
        }
   
        private Cikk _CIKK;
        public Cikk CIKK
        {
            get { return (_CIKK); }
            set
            {
                if (_CIKK != value) { isModified = true; }
                _CIKK = value;

            }
        }
       
        public string CIKK_NEV
        {
            get
            {
                return (_CIKK.MEGNEVEZES);
            }

        }
        
        private double _MENNYISEG;
        public double MENNYISEG
        {
            get { return (_MENNYISEG); }
            set { if (_MENNYISEG != value) { isModified = true; } _MENNYISEG = value; }
        }
      
        
        private double _BESZ_AR;
        public double BESZ_AR
        {
            get { return (_BESZ_AR); }
            set { if (_BESZ_AR != value) { isModified = true; } _BESZ_AR = value; }
        }
        
        #endregion

        public MegrendelesSor(Cikk c, int pfej_id)
        {

            _SOR_ID = -1;
            isModified = false;
            _CIKK = c;
            _BESZ_AR = _CIKK.UTOLOS_BESZ_AR;
            _MENNYISEG = 0;
            _FEJ_ID = pfej_id;
            
        }

        public MegrendelesSor(int pSor_id, SqlConnection c)
        {
            if (c.State == ConnectionState.Closed) { c.Open(); }
            SqlCommand cmd = new SqlCommand();

                    


            cmd.Connection = c;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT SOR_ID ,FEJ_ID ,CIKK_ID, BESZ_AR ,MENNYISEG FROM MEGRENDELES_SOR WHERE SOR_ID =" + pSor_id.ToString();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                _SOR_ID = (int)rdr["SOR_ID"];
                _FEJ_ID = (int)rdr["FEJ_ID"];
                _CIKK = new Cikk((int)rdr["CIKK_ID"],true,new SqlConnection(DEFS.ConSTR));
                _BESZ_AR = (double)rdr["BESZ_AR"];
                _MENNYISEG = (double)rdr["MENNYISEG"];

            }
            c.Close();
            isModified = false;

        }

        public void Save()
        {
            SqlConnection c = new SqlConnection(DEFS.ConSTR);
            c.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;

            int new_p_id = _SOR_ID;

            switch (_SOR_ID)
            {
                case -1:
                    {
                        //új rekord!!

                        
                        cmd.CommandText = "INSERT INTO MEGRENDELES_SOR " +
                                               "(FEJ_ID " +
                                               ",CIKK_ID " +
                                               ",MENNYISEG " +
                                               ",BESZ_AR " +
                                               " ) " +
                                         "VALUES " +
                                               "(@FEJ_ID " +
                                               ",@CIKK_ID " +
                                               ",@MENNYISEG " +
                                               ",@BESZ_AR " +
                                               " ) SET @newid = SCOPE_IDENTITY()";
                        cmd.Parameters.Add(new SqlParameter("newid", SqlDbType.Int));
                        cmd.Parameters["newid"].Direction = ParameterDirection.Output;

                        break;
                    }
                default:
                    {
                        
                        cmd.CommandText = "UPDATE MEGRENDELES_SOR " +
                                           "SET FEJ_ID = @FEJ_ID " +
                                           "   ,CIKK_ID = @CIKK_ID " +
                                           "   ,MENNYISEG= @MENNYISEG " +
                                           "   ,BESZ_AR = @BESZ_AR " +
                                          
                                         "WHERE  SOR_ID= @SOR_ID";
                        cmd.Parameters.Add(new SqlParameter("SOR_ID", SqlDbType.Int));
                        cmd.Parameters["SOR_ID"].Value = _SOR_ID;
                        break;
                    }
            }

            cmd.Parameters.Add(new SqlParameter("FEJ_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("CIKK_ID", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("MENNYISEG", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("BESZ_AR", SqlDbType.Float));

            cmd.Parameters["FEJ_ID"].Value = _FEJ_ID;
            cmd.Parameters["CIKK_ID"].Value = _CIKK.CIKK_ID;
            cmd.Parameters["MENNYISEG"].Value = _MENNYISEG;
            cmd.Parameters["BESZ_AR"].Value = _BESZ_AR;
            

            //try
            //{
            cmd.ExecuteNonQuery();


            if (_SOR_ID == -1)
            {
                new_p_id = (int)cmd.Parameters["newid"].Value;
            }
            //DEFS.SendSaveErrMessage(new_p_id.ToString() +" : Sikertelen bevlételezés mentés");

            _SOR_ID = new_p_id;

            c.Close();
        }
    }

}