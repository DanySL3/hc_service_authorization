using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Usuario
{
    public class ListarUsuarioEntity
    {
        public int usuario_id { get; set; }

        public int cargo_id { get; set; }

        public string nombre { get; set; }

        public string documento_numero { get; set; }

        public string cargo { get; set; }

        public string correo { get; set; }

        public string usuario { get; set; }

        public int usuario_estado_id { get; set; }

        public string usuario_estado { get; set; }
    }
}
