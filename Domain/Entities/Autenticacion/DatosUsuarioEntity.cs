using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Autenticacion
{
    public class DatosUsuarioEntity
    {
        public int? usuario_id { get; set; }

        public string? cNombreCompleto { get; set; }

        public string? cNombreUsuario { get; set; }

        public string? cFoto { get; set; }

        public bool lAdmin { get; set; }
    }
}
