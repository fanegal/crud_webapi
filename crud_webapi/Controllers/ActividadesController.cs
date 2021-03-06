using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using crud_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crud_webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        [HttpPost]
        public Object nueva_actividad(add_actividad item)
        {
            B_Activity neg;
            entidades.Activity obj;
            try
            {
                resultado result = new resultado();
                if (ModelState.IsValid)
                {
                    neg = new B_Activity();
                    obj = new entidades.Activity();

                    obj.Schedule = item.calendario;
                    obj.Title = item.titulo;


                    neg.nueva_actividad(item.id_propiedad, obj);
                    if (neg.bit_error)
                    {
                        result.bit_error = true;
                        result.mensaje = neg.error.Message.ToString();
                    }
                    else
                    {
                        result.bit_error = false;
                        result.mensaje = string.Format("{0} su id es:{1}", "nueva actividad registrada ", neg.ultimo_id);

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
        [HttpPut("{id}")]
        public object cancelar(int id)
        {
            B_Activity neg;
            try
            {
                resultado result = new resultado();
                neg = new B_Activity();
                neg.cancelar_actividad(id);
                if (neg.bit_error)
                {
                    result.bit_error = true;
                    result.mensaje = neg.error.Message.ToString();
                }
                else
                {
                    result.bit_error = false;
                    result.mensaje = "La actividad fue cancelada";

                }

                return result;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPut]
        public object reagendar_actividad(change_actividad item)
        {
            B_Activity neg;
            try
            {
                resultado result = new resultado();

                if (ModelState.IsValid)
                {
                    neg = new B_Activity();
                    neg.reagendar_actividad(item.id, item.calendario);
                    if (neg.bit_error)
                    {
                        result.bit_error = true;
                        result.mensaje = neg.error.Message.ToString();
                    }
                    else
                    {
                        result.bit_error = false;
                        result.mensaje = "La actividad fue reagendada";


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

        [HttpGet]
        public IEnumerable<object> getActividades(lista_filtros filtros)
        {
            IEnumerable<lista_filtros> result;
            B_Activity neg;
            try
            {
                neg = new B_Activity();
                List<data_access.lista> datos = neg.GetList(filtros.regla, filtros.status, filtros.fechainicio_calendario, filtros.fechafin_calendario).ToList();

                datos.ForEach(s =>
                {
                    s.survey = "/api/Encuestas/" + s.survey;

                });

                return datos;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
