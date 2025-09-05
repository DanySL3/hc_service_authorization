using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sistema
{
    public class DatosAplicacionEntity
    {
        public int sistema_id { get; set; }

        public string nombre { get; set; }

        public string icono { get; set; }

        public string url { get; set; }

        public int codigo { get; set; } = 0;
    }
}
