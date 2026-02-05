using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Autenticacion
{
    public class ListaPrivilegiosEntity
    {
        public int usuario_id { get; set; }
        public string nombre { get; set; }
        public string documento_numero { get; set; }
        public string correo { get; set; }
        public string cargo { get; set; }
        public int sistema_id { get; set; }
        public string sistema { get; set; }
        public string fechaIniAcceso { get; set; }
        public string fechaFinAcceso { get; set; }
        public int perfil_id { get; set; }
        public string perfil { get; set; }
    }
}
