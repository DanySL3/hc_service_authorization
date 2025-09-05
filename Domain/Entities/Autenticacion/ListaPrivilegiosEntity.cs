using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Autenticacion
{
    public class ListaPrivilegiosEntity
    {
        public int nCodigoCliente { get; set; }
        public string cNombre { get; set; }
        public int sistema_id { get; set; }
        public string cSistema { get; set; }
        public string cFechaIniAcceso { get; set; }
        public string cFechaFinAcceso { get; set; }
        public int idPerfil { get; set; }
        public string cPrivilegio { get; set; }
    }
}
