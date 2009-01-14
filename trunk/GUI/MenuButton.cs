﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BusinessLogic;

namespace GUI
{
    public class CikkcsopButton : System.Windows.Forms.Button
    {


        public Cikkcsoport _Cikkcsoport;
        
        public CikkcsopButton(Cikkcsoport aCcs)
        {
            _Cikkcsoport = aCcs;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }


     
    }

    public class HelyButton : System.Windows.Forms.Button
    {
        public Hely _Hely;
        public HelyButton(Hely aHely)
        {
            _Hely = aHely;

        }
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
        }

    }

    public class OtherFButton : System.Windows.Forms.Button
    {
        public OTF _OTF;
        public OtherFButton(OTF aOtf)
        {
            _OTF = aOtf;
        }
    }
}