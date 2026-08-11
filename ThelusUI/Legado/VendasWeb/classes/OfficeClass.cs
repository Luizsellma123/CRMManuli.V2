using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Text;

namespace VendasWeb.GerencialVendas
{
    public class OfficeClass
    {
        public GridView GridView1 { get; set; }

        public string ExportDataSetToExcel()
        {
            string Retorno = "";

            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
            ws.Name = "Pasta";


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int j = 0; j < GridView1.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range ce = (ws.Cells[i + 1, j + 1] as Microsoft.Office.Interop.Excel.Range);
                    ce.Value2 = GridView1.Rows[i].Cells[j].Text.ToString();
                }
            }

            wb.SaveAs("c:\\excelTeste.xlsx", Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excel.Quit();

            Retorno = "O arquivo C:\\excelTeste.xlsx foi criado com sucesso. ";

            return Retorno;            
        }
    }
}