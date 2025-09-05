using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Menu
{
    public class DatosMenusExternoEntity
    {
        public int id { get; set; }
        public int menu_padre_id { get; set; }
        public string menu { get; set; }
        public string descripcion { get; set; }
        public string url { get; set; }
        public bool esAsignado { get; set; }

        public List<DatosMenusExternoEntity> lstMenuHijos { get; set; }
    }
}
