using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OWDD_AprovarAutorizacaoClass
    {
        public string Status { get; set; }
        public List<WDD1_AprovarAutorizacaoClass> ApprovalRequestDecisions { get; set; }
    }
}