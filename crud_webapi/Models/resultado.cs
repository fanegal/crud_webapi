using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_webapi.Models
{
    public class resultado
    {
        public string mensaje { get; set; }
        public bool bit_error { get; set; }
        public String error { get; set; }
        public object adicional { get; set; }
    }
}
