﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{

    public class Syspar
    {
        public ParamCodes PARAM_CODE;



        public string PARAM_NAME;
        public string VALUE_S;
        public int VALUE_I;
        public double VALUE_F;

        public Syspar(string pName, string pVs, int pVI, double pVD)
        {
            PARAM_NAME = pName;
            VALUE_F = pVD;
            VALUE_I = pVI;
            VALUE_S = pVs;


        }

        public void Save()
        {

        }
    }

    
    public class SysParList
    {


        public List<Syspar> lSyspar = new List<Syspar>();

        public SysParList()
        {

            SqlConnection sc = new SqlConnection(DEFS.ConSTR);
            sc.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = sc;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT PARAM_NAME, isnull(PARAM_VALUE_S,'') as PARAM_VALUE_S, isnull(PARAM_VALUE_I,-1) as PARAM_VALUE_I, isnull(PARAM_VALUE_F,-1.0) as PARAM_VALUE_F FROM SYSPAR";

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Syspar t = new Syspar((string)rdr["PARAM_NAME"],
                                (string)rdr["PARAM_VALUE_S"],
                                (int)rdr["PARAM_VALUE_I"],
                                (double)rdr["PARAM_VALUE_F"]);
                lSyspar.Add(t);
            }
            rdr.Close();
            sc.Close();
        }


        public int GetIntValue(string pParamName)
        {
            int ret = -1;
            var ret__Syspar =
                from c in lSyspar
                where (c.PARAM_NAME == pParamName)
                select c;
            ret__Syspar.Each(c => ret = c.VALUE_I);


            return (ret);
        }

        public string GetStrValue(string pParamName)
        {
            string ret = "";
            var ret__Syspar =
                from c in lSyspar
                where (c.PARAM_NAME == pParamName)
                select c;
            ret__Syspar.Each(c => ret = c.VALUE_S);


            return (ret);
        }

        public string GetStrValueFilterInt(string pParamName, int iFilter)
        {
            string ret = "";
            var ret__Syspar =
                from c in lSyspar
                where (c.PARAM_NAME == pParamName && c.VALUE_I == iFilter)
                select c;
            ret__Syspar.Each(c => ret = c.VALUE_S);


            return (ret);
        }
    }
    
    public enum ParamTypes
    {
        egesz,
        szoveg,
        tizedes,
        image
    }
    public static class Syspar2
    {
        public static Object GetValue(ParamCodes c)
        {
            string field_name = "";
            switch ((ParamTypes)Enum.Parse(typeof(ParamCodes), c.ToString()))
            {
                case ParamTypes.egesz:
                    field_name = "PARAM_VALUE_I";
                    break;
                case ParamTypes.szoveg:
                    field_name = "PARAM_VALUE_S";
                    break;
                case ParamTypes.tizedes:
                    field_name = "PARAM_VALUE_F";
                    break;
                case ParamTypes.image:
                    field_name = "PARAM_VALUE_IMAGE";
                    break;
                default:
                    break;
            }

            return (Object)(field_name +"-->" +GetType(c));

        }

        public static Type GetType(ParamCodes cc)
        {
            switch (((ParamTypes)Enum.Parse(typeof(ParamCodes), cc.ToString())))
            {
                case ParamTypes.egesz:
                    return typeof(Int16);
                    break;
                case ParamTypes.szoveg:
                    return (typeof(String));
                    break;
                case ParamTypes.tizedes:
                    return typeof(Double);
                    break;
                case ParamTypes.image:
                    return typeof(String);
                    break;
                default:
                    return typeof(Object);
                    break;
            }


        }


    }

    public enum ParamCodes
    {
       SHOW_ORDER_BEFORE = ParamTypes.egesz,
       CEG_NEV = ParamTypes.szoveg,
       CEG_CIM = ParamTypes.szoveg,
       BLOKK_LABLEC1 = ParamTypes.szoveg,
       BLOKK_LABLEC2 = ParamTypes.szoveg,
       BLOKK_LABLEC3 = ParamTypes.szoveg,
       LELTAR_KOROK_SZAMA = ParamTypes.egesz,
       AUTO_PRINT_BLOKK = ParamTypes.egesz,
       OSSZ_REPORT_FORMAT = ParamTypes.szoveg

    }
    


   
}
