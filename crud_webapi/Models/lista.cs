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

    public class lista
    {
        public int ID { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public string status { get; set; }
        public string condition { get; set; }
        public propiedad property { get; set; }
        public string survey { get; set; }
    }


    public class propiedad
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string address { get; set; }

    }
}
