using System;
using System.Linq;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace VendasWeb.classes
{
    public class ExcelDataTableClass
    {
        int primeiroIndice = 0;
        int ultimoIndice = 0;

        public System.Data.DataTable LerExcel(string path)
        {
            Excel.Application objXL = null;
            Excel.Workbook objWB = null;
            Excel.Worksheet objSHT = null;
            Excel.Range usedRange = null;
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                objXL = new Excel.Application();
                objWB = objXL.Workbooks.Open(path);
                objSHT = objWB.Worksheets[1];

                usedRange = objSHT.UsedRange;
                int rows = usedRange.Rows.Count;
                int cols = usedRange.Columns.Count;

                // Criando colunas
                for (int c = 1; c <= cols; c++)
                {
                    Excel.Range cell = usedRange.Cells[1, c] as Excel.Range;
                    string colname = Convert.ToString(cell?.Text) ?? Convert.ToString(cell?.Value2) ?? $"Col{c}";
                    dt.Columns.Add(colname);
                    Marshal.ReleaseComObject(cell);
                }

                // Criando linhas
                for (int r = 2; r <= rows; r++)
                {
                    DataRow dr = dt.NewRow();
                    bool isEmpty = true;

                    for (int c = 1; c <= cols; c++)
                    {
                        Excel.Range cell = usedRange.Cells[r, c] as Excel.Range;

                        string cellValue = string.Empty;

                        try
                        {
                            // 1) Prioriza o texto exibido (formatação do Excel) — evita shifts de data/hora.
                            string displayed = Convert.ToString(cell?.Text)?.Trim();
                            if (!string.IsNullOrEmpty(displayed))
                            {
                                cellValue = displayed;
                            }
                            else
                            {
                                // 2) Fallback para Value2 (pode ser double (OADate), DateTime, ou texto)
                                object rawValue = cell?.Value2;
                                if (rawValue != null)
                                {
                                    if (rawValue is double) // normalmente data/hora no Excel vem como double
                                    {
                                        try
                                        {
                                            DateTime dtValue = DateTime.FromOADate((double)rawValue);
                                            // Formata para dia/mês/ano. Se quiser preservar hora, ajuste aqui.
                                            cellValue = dtValue.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
                                        }
                                        catch
                                        {
                                            cellValue = rawValue.ToString();
                                        }
                                    }
                                    else if (rawValue is DateTime) // caso retorne DateTime diretamente
                                    {
                                        DateTime dtValue = (DateTime)rawValue;
                                        cellValue = dtValue.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
                                    }
                                    else
                                    {
                                        // string ou outro tipo
                                        cellValue = rawValue.ToString().Trim();
                                    }
                                }
                            }

                            // 3) Último recurso: tentar parse com pt-BR se estiver num formato ambíguo
                            if (string.IsNullOrEmpty(cellValue) && cell?.Value != null)
                            {
                                string rawText = Convert.ToString(cell.Value)?.Trim();
                                if (!string.IsNullOrEmpty(rawText))
                                {
                                    DateTime parsed;
                                    if (DateTime.TryParse(rawText, new CultureInfo("pt-BR"), DateTimeStyles.None, out parsed))
                                    {
                                        cellValue = parsed.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
                                    }
                                    else
                                    {
                                        cellValue = rawText;
                                    }
                                }
                            }
                        }
                        finally
                        {
                            // garante liberação mesmo em caso de exceção
                            dr[c - 1] = cellValue;
                            if (!string.IsNullOrEmpty(cellValue))
                                isEmpty = false;

                            Marshal.ReleaseComObject(cell);
                        }
                    }

                    if (!isEmpty)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            finally
            {
                if (objWB != null)
                {
                    objWB.Close(false);
                    Marshal.ReleaseComObject(objWB);
                }

                if (objXL != null)
                {
                    objXL.Quit();
                    Marshal.ReleaseComObject(objXL);
                }

                if (usedRange != null)
                {
                    Marshal.ReleaseComObject(usedRange);
                }

                if (objSHT != null)
                {
                    Marshal.ReleaseComObject(objSHT);
                }

                objWB = null;
                objXL = null;
                usedRange = null;
                objSHT = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return dt;
        }

        public System.IO.MemoryStream BaixarExcelDeDataTable(System.Data.DataTable dataTable)
        {
            //Get properties using reflection.
            System.Data.DataSet ds = new System.Data.DataSet("New_DataSet");

            for (int i = 0; i < 100; i++)
            {
                dataTable.Rows.Add(Enumerable.Repeat(" ", dataTable.Columns.Count).ToArray());
            }

            ds.Tables.Add(dataTable);

            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            ExcelLibrary.DataSetHelper.CreateWorkbook(stream, ds);

            return stream;
        }

        protected string VerificarColunas(System.Data.DataTable dt, List<string> ColunasEsperadas)
        {
            string erro = "";

            foreach (string coluna in ColunasEsperadas)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != coluna)
                    {
                        erro = $"Erro: A coluna '{coluna}' não foi encontrada. Verifique se não há espaços ou caracteres entre as palavras."; ;
                    }
                    else
                    {
                        erro = "";

                        break;
                    }
                }

                if (erro != "") break;
            }

            return erro;
        }

        protected string VerificarLinhasVazias(System.Data.DataTable dt)
        {
            primeiroIndice = -1;
            ultimoIndice = -1;

            // Encontrar primeiro e último índice com dados
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                if (row.ItemArray.Any(item => item != DBNull.Value && !string.IsNullOrWhiteSpace(item.ToString())))
                {
                    if (primeiroIndice == -1)
                        primeiroIndice = i;

                    ultimoIndice = i;
                }
            }

            // Se encontrou dados, verifica se há linhas vazias no meio
            if (primeiroIndice != -1)
            {
                for (int i = primeiroIndice; i <= ultimoIndice; i++)
                {
                    DataRow row = dt.Rows[i];

                    bool isRowEmpty = row.ItemArray.All(item => item == DBNull.Value || string.IsNullOrWhiteSpace(item.ToString()));

                    if (isRowEmpty)
                    {
                        return "Erro: Existem linhas vazias no meio dos dados.";
                    }
                }
            }

            return string.Empty;
        }

        List<string> errosList = new List<string>();

        public string Pre_Verificacao_Excel_Importacao_Chamados_DataTable(System.Data.DataTable ExcelUsuarioDataTable)
        {
            List<string> ColunasEsperadas = new List<string>
            {
                "Data", "Solicitante", "Responsavel", "Classificacao", "Sistema", "Status", "Prioridade", "Setor", "Assunto", "Descricao"
            };            

            string erro = VerificarColunas(ExcelUsuarioDataTable, ColunasEsperadas);

            if (erro != "") errosList.Add(erro);

            erro = VerificarLinhasVazias(ExcelUsuarioDataTable);

            if (erro != "") errosList.Add(erro);

            if (errosList.Count > 0)
            {
                StringBuilder erros = new StringBuilder();

                // Ordena os erros pelo número da linha
                var errosOrdenados = errosList
                    .OrderBy(e =>
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(e, @"Erro na linha (\d+)");
                        return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
                    })
                    .ToList();

                for (int i = 0; i < 10; i++)
                {
                    if (i >= errosOrdenados.Count) break;

                    erros.AppendLine(errosOrdenados[i]);

                    if (i < errosOrdenados.Count - 1) erros.AppendLine("<br />");
                }

                return erros.ToString();
            }

            return "";
        }
    }
}