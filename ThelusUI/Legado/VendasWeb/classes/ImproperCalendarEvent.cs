using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Do not use this object, it is used just as a go between between javascript and asp.net
public class ImproperCalendarEvent
{
    #region Campos Base
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public bool allDay { get; set; }
    public string className { get; set; }
    #endregion


    public int IDAgendamento { get; set; }
    public string DataInicio { get; set; }
    public string HoraInicio { get; set; }
    public string DataFinal { get; set; }
    public string HoraFinal { get; set; }
    public string UsuCod { get; set; }
    public string UsuCodGestor { get; set; }
    public int IdTipoAgendamento { get; set; }
    public string DescricaoTipoAgendamento { get; set; }
    public string CondicaoVisita { get; set; }
    public decimal idLembreteUm { get; set; }
    public string LembreteUm { get; set; }
    public decimal idLembreteDois { get; set; }
    public string LembreteDois { get; set; }
    public string DescricaoCompromisso { get; set; }
    public string EntCod { get; set; }
    public string EntNome { get; set; }
    public string EntCpfCgc { get; set; }
    public string Endereco { get; set; }
    public string ContatoNome { get; set; }
    public string ContatoTelefone { get; set; }
    public string ContatoEmail { get; set; }
    public string LinhaProdutoQuantidadeStretch { get; set; }
    public string LinhaProdutoQuantidadeFitaPP { get; set; }
    public string LinhaProdutoQuantidadeFitaImpressa { get; set; }

    public string VinculaEntidade { get; set; }

    public string ComRepresentante { get; set; }
    public string MeioTransporte { get; set; }
    public decimal Km { get; set; }
    public decimal ValorEstimadoViagem { get; set; }
    public string StatEntComercial { get; set; }
    public string EntStatDescr { get; set; }
    public string DataUltimaVisita { get; set; }
    public int EstimativaVendaStretch { get; set; }
    public int EstimativaVendaFitaPP { get; set; }
    public int EstimativaVendaFitaImpressa { get; set; }
    public string NFHoraSaida { get; set; }
    public decimal NFValTotNota { get; set; }
    public decimal TotalVendaAnual { get; set; }

    public string ClasseCliente { get; set; }
    public string ItensNF { get; set; }

    
    

    
}
