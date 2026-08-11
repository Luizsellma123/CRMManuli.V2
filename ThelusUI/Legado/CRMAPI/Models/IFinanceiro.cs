using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CRMAPI.Models
{
    interface IFinanceiro
    {
        string AtualizaAnalisarEsboco();
        DataTable RecuperaAutorizacoesEsbocoSAP();
    }
}
