using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZapatos.DTO.Response
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Response { get; set; }
        public int TotalElements { get; set; }
        public Error Error { get; set; }
    }
}
