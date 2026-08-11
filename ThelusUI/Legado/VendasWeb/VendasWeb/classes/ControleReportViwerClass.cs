using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Reflection;

namespace VendasWeb.GerencialVendas
{
    public class ControleReportViwerClass
    {

        /*Ocultar Opções ReportViwer Exemplo: PDF,WORD,Excel
          Passe o Report e qual opção deseja ocultar
         */
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
            {
                if (extension.Name.ToUpper().Contains(strFormatName.ToUpper()))
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }

    }
}