using crud_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Business;

namespace crud_webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadesController : ControllerBase
    {
        [HttpPost]
        public Object nueva_actividad(add_propiedad item)
        {
            B_Property neg;
            entidades.Property obj;
            try
            {
                resultado result = new resultado();
                if (ModelState.IsValid)
                {
                    neg = new B_Property();
                    obj = new entidades.Property();

                    obj.Title = item.titulo;
                    obj.Description = item.descripcion;
                    obj.Address = item.direccion;

                    neg.nuevo(obj);
                    if (neg.bit_error)
                    {
                        result.bit_error = true;
                        result.mensaje = neg.error.Message.ToString();
                    }
                    else
                    {
                        result.bit_error = false;
                        result.mensaje = string.Format("{0} su id es:{1}", "nueva propiedad registrada ", neg.ultimo_id);

                    }
                }
                else
                {
                    result.bit_error = true;
                    result.error = "El modelo no es valido";
                }
                return result;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
