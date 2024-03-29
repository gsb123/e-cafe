﻿using System;
using System.Data;
using System.Drawing;
using BusinessLogic;
using ReportPrinting;

namespace e_Cafe.Reports
{
    public class BlokkReport: IReportMaker
	{
        int _SzamlaId;

        public BlokkReport(int SzamlaId)
        {
            _SzamlaId = SzamlaId;
        }

        public void MakeDocument(ReportDocument reportDocument)
        {
            // Always reset the text styles if you have multiple methods that
            // set them
            int height = 0;
            int sor_magas = 6;
            TextStyle.ResetStyles();
            SectionBox box;
            LinearSections contents;
            //TextStyle.Normal.BackgroundBrush = Brushes.Beige;
            Szamla iSzamla = new Szamla(_SzamlaId);
            // Create a ReportBuilder object that assists with building a report
            ReportBuilder builder = new ReportBuilder(reportDocument);

            builder.CurrentDocument.DocumentUnit = GraphicsUnit.Millimeter;
            // Before adding sections, a layout must be started.  
            // We are using a linear layout - vertically, which means
            // each new section starts below the last one.

            builder.HorizLineMargins = 0.2f;

            builder.StartLinearLayout(Direction.Vertical);



            builder.AddPageHeader("ALL-IN Cafe", String.Empty, iSzamla.SZAMLA_DATUMA.ToShortDateString() + " " + iSzamla.SZAMLA_DATUMA.ToLongTimeString());

            height += sor_magas;

            #region fejlec
            builder.StartLayeredLayout(false, false);

            // Add various text sections in different headings

            box = new SectionBox();
            box.Width = 80;
            box.Height = 10;
            box.OffsetLeft = 0;
            box.OffsetTop = 0;

            //box.Border.
            //box.Background = Brushes.Ivory;
            contents = new LinearSections();
            contents.AddSection(new SectionText((string)Syspar2.GetValue(ParamCodes.CEG_NEV), TextStyle.Heading1));
            contents.AddSection(new SectionText((string)Syspar2.GetValue(ParamCodes.CEG_CIM), TextStyle.Normal));
            box.AddSection(contents);
            builder.AddSection(box);

            height += 10;

            // Logo
            box = new SectionBox();
            box.Width = 40;
            box.Height = 10;
            box.OffsetLeft = 80;
            box.OffsetTop = 0;
            box.HorizontalAlignment = HorizontalAlignment.Center;
            // box.VerticalAlignment = VerticalAlignment.Bottom;
            //box.Border = reportDocument.NormalPen;
            SectionImage image;
            try {
                 image = new SectionImage(Image.FromFile((string)Syspar2.GetValue(ParamCodes.BLOKK_LOGO_PATH)));
            } catch (Exception ix) {
                 DEFS.ExLog(ix.Message + "\n" + ix.StackTrace);
                 image = new SectionImage(global::GUI.Properties.Resources.logo);
            }
            
            
            //image.Transparency = 50;
            //image.PreserveAspectRatio = false;
            box.AddSection(image);
            builder.AddSection(box);

            // Finish a layout that we started
            // builder.FinishLayeredLayout();
            // 
            

            builder.FinishLayeredLayout();

            #endregion

            builder.AddText("Blokk sorszáma: "+ iSzamla.SZAMLA_SORSZAM.PadLeft(7,'0'), TextStyle.Normal);
            builder.AddText(" ");

            height += 2 * sor_magas;


            builder.StartLayeredLayout(false, false);
            if (DateTime.Now >= Convert.ToDateTime(new DateTime(2010, 1, 1)))
            {
                // Tesztüzem
                SectionBox box_teszt = new SectionBox();
                box_teszt.WidthPercent = 30;
                //box.Height = 1;
                box_teszt.HorizontalAlignment = HorizontalAlignment.Center;
                box.VerticalAlignment = VerticalAlignment.Top;
                //box.Border = reportDocument.NormalPen;

                SectionImage image_teszt = new SectionImage(global::GUI.Properties.Resources.tesztuzem);
                image_teszt.Transparency = 80;
                //image.PreserveAspectRatio = false;
                box_teszt.AddSection(image_teszt);
                builder.AddSection(box_teszt);
            }

           


            #region sorok
            DataView dv = iSzamla.GetBlokkDataView();
            builder.DefaultTablePen = null;
            
            // ide még kell egy faktor ami a sortöréseket határozza meg.

            height += sor_magas * dv.Count;

            builder.AddTable(dv, true, 100);

            builder.Table.InnerPenHeaderBottom = reportDocument.NormalPen;
            builder.Table.InnerPenRow = new Pen(Color.Gray, reportDocument.ThinPen.Width);
            builder.Table.OuterPenBottom = new Pen(Color.Gray, reportDocument.ThinPen.Width);

            builder.AddColumn(dv.Table.Columns[0], "Db.", 8, false, false, HorizontalAlignment.Left);
            builder.AddColumn(dv.Table.Columns[1], "Termék", 30, false, false, HorizontalAlignment.Left);
            builder.AddColumn(dv.Table.Columns[2], "Összeg", 40, false, false, HorizontalAlignment.Right);

            //dt.Columns.Add(, typeof(int));
            //dt.Columns.Add("Cikk", typeof(string));
            //dt.Columns.Add("Összeg", typeof(double));


            // builder.AddAllColumns(30.0f, true, true);

            builder.CurrentSection.HorizontalAlignment = HorizontalAlignment.Left;



            #endregion
            builder.FinishLayeredLayout();
            
            #region végösszesen
            DataView dv2 = iSzamla.GetBlokkOsszegDataView();
            builder.DefaultTablePen = null;
            builder.AddTable(dv2, true, 100);

            height += sor_magas * dv2.Count;

            builder.AddColumn(dv2.Table.Columns[0], " ", 50, false, false, HorizontalAlignment.Right);
            builder.AddColumn(dv2.Table.Columns[1], " ", 30, false, false, HorizontalAlignment.Right);


            #endregion

            


            builder.AddText(" ");


            builder.AddText((string)Syspar2.GetValue(ParamCodes.BLOKK_LABLEC1), TextStyle.Normal);

            builder.AddText((string)Syspar2.GetValue(ParamCodes.BLOKK_LABLEC2), TextStyle.Normal);
            builder.AddText((string)Syspar2.GetValue(ParamCodes.BLOKK_LABLEC3), TextStyle.Normal);
            //builder.AddText((string)Syspar2.GetValue(ParamCodes.BLOKK_LABLEC4), TextStyle.Normal);

            height += sor_magas * ((Syspar2.GetValue(ParamCodes.BLOKK_LABLEC1).ToString().Length / 45) + 1);
            height += sor_magas * ((Syspar2.GetValue(ParamCodes.BLOKK_LABLEC2).ToString().Length / 45) + 1);
            height += sor_magas * ((Syspar2.GetValue(ParamCodes.BLOKK_LABLEC3).ToString().Length / 45) + 1);
            //height += sor_magas * (((string)Syspar2.GetValue(ParamCodes.BLOKK_LABLEC4).Length / 45) + 1);
            height += sor_magas;
            builder.CurrentDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", DEFS.MMtoInch(75), DEFS.MMtoInch(height));
            builder.CurrentDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(DEFS.MMtoInch(1), DEFS.MMtoInch(1), DEFS.MMtoInch(1), DEFS.MMtoInch(1));
            
            builder.FinishLinearLayout();

            
            
        }

        


    }
}
