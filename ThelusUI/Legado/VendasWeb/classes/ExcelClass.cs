using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class ExcelClass
    {
        public string VerificaArquivoExcel(System.Web.UI.WebControls.FileUpload ArquivoFileUpload)
        {
            try
            {
                string extensionArquivo = "";

                if (ArquivoFileUpload.HasFile == true)
                {
                    extensionArquivo = System.IO.Path.GetExtension(ArquivoFileUpload.FileName);

                    if (extensionArquivo != ".xls" && extensionArquivo != ".xlsx")
                        return "Somente permitido com a extensão .xls ou .xlsx !";
                }
                else
                {
                    return "Selecione um arquivo.";
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}