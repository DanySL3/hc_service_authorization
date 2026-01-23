using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Usuario
{
    public class ListarCargosEntity
    {
        public int cargo_id { get; set; }
        public int cargo_padre_id { get; set; }
        public string nombre { get; set; }
    }
}
