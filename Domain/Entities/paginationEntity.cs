using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class paginationEntity<T>
    {
        public int page_index {  get; set; }
        public int page_size {  get; set; }
        public int total_count {  get; set; }
        public List<T> data { set; get; } = new List<T>();
    }
}
