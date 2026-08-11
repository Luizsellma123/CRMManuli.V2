using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class CrmGrupoUsuarioClass
    {

        public int IDGrupo { get; set; }
        public string Nome { get; set; }
        public int IDUsuario { get; set; }
        public string Administrador { get; set; }

        public string Status { get; set; }

    }
}