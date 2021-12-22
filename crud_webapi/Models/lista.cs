using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_webapi.Models
{

    public class lista_filtros
    {
        public bool regla { get; set; }
        public string? status { get; set; }
        public DateTime? fechainicio_calendario { get; set; }
        public DateTime? fechafin_calendario { get; set; }
    }

 

    
}
