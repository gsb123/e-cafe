﻿using System;
using System.Collections.Generic;
using ReportPrinting;


namespace e_Cafe.Reports
{
    public class doPrinting
    {
        private ReportPrinting.ReportDocument reportDocument1;
        private ReportPrinting.PrintControlToolBar printControlToolBar1 = new ReportPrinting.PrintControlToolBar();

        public doPrinting()
        {

            reportDocument1 = new ReportPrinting.ReportDocument();

            reportDocument1.Body = null;
            reportDocument1.PageFooter = null;
            reportDocument1.PageHeader = null;
            
            reportDocument1.DocumentUnit = System.Drawing.GraphicsUnit.Millimeter;


            // 
            // printControlToolBar1
            // 
            printControlToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            printControlToolBar1.Document = reportDocument1;
            printControlToolBar1.Location = new System.Drawing.Point(0, 0);
            printControlToolBar1.Name = "printControlToolBar1";
            printControlToolBar1.ClientSize = new System.Drawing.Size(1024,768);
            printControlToolBar1.TabIndex = 2;
            //printControlToolBar1.Load += new System.EventHandler(printControlToolBar1_Load);
            //pintControlToolBar1.Printing += new System.EventHandler(printControlToolBar1_Printing);

        }

        public void setReportMaker(IReportMaker rp)
        {
            reportDocument1.ReportMaker = rp;
        }

        public void doPreview()
        {
            //printControlToolBar1.
            printControlToolBar1.Preview(null, null);
            
        }

        public void doPrint()
        {
            printControlToolBar1.PrintInBackground = true;

            printControlToolBar1.Print(null, null);

        }

        public void doPrintDefault()
        {
            printControlToolBar1.PrintInBackground = true;

            printControlToolBar1.PrintDefault(null, null);

        }

    }
}
