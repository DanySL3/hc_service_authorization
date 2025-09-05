using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Menu
{
    public class DatosMenusEntity
    {
        public int id { get; set; }

        public int menu_padre_id { get; set; }

        public int menu_tipo_id { get; set; }

        public string menu { get; set; }

        public string url { get; set; }

        public string icono { get; set; }

        [JsonIgnore]
        public int orden { get; set; }

    }
}
