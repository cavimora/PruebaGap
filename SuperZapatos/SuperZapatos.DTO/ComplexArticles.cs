using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZapatos.DTO
{
    public class ComplexArticles<T>
    {
        public T Articles { get; set; }
        public List<StoreDto> Stores { get; set; }
    }
}
