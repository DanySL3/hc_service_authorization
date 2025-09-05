using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agencia
{
    public class DatosAgenciaEntity
    {
        public int agencia_id { get; set; }

        public string nombre { get; set; }

        public bool? esPrincipal { get; set; }
    }
}
