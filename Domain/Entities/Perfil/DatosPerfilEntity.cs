using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Perfil
{
    public class DatosPerfilEntity
    {
        public int perfil_id { get; set; }

        public string perfil { get; set; }

        public int codigo { get; set; }

        public bool? esPrincipal { get; set; }
    }
}
