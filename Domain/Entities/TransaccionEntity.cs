using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TransaccionEntity
    {
        public bool Code { get; set; }
        public int ID { get; set; }
        public string Message { get; set; } = "";
    }
}
