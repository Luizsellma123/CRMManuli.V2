using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class FiltroClass
    {

        public string DropOpcaoFiltro { get; set; } //Opcao do Combo disponivel na tela
        public string TextoFiltro { get; set; } //Valor digitado 

        public string EmpCod { get; set; }
        public string PedVendaStatDescr { get; set; }
        public string PedVendaTipo { get; set; }

       
        

    }
}