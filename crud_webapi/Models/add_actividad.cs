using System; 
using System.ComponentModel.DataAnnotations; 

namespace crud_webapi.Models
{
    public class add_actividad
    {
        [Required]
        public int id_propiedad { get; set; }

        [Required]
        public DateTime calendario { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string titulo { get; set; }
    }
}
