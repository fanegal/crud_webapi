using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crud_webapi.Models
{
    public class change_actividad
    {
        [Required]
        public int id { get; set; }
        [Required]
        public DateTime calendario { get; set; }
    }
}
