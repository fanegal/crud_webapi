using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class B_Activity
    {
        private data_access.Activity data;
        public bool bit_error;
        public Exception error;
        public B_Activity()
        {
            data = new data_access.Activity();
        }


        public void nueva_actividad(int id_propiedad, entidades.Activity actividad)
        {
            B_Property neg_prop;
            try
            {
                neg_prop = new B_Property();
                if (neg_prop.propiedadActiva(id_propiedad))
                {
                    if (data.getList(f => f.PropertyId == id_propiedad
                                      && f.Schedule >= actividad.Schedule.AddHours(-1)
                                      && f.Schedule <= actividad.Schedule.AddHours(1)
                                      && f.Status == "active").Count == 0)

                    {
                        var indice = data.getAll().Count;
                        if (indice > 0)
                        {
                            actividad.Id = indice + 1;
                        }
                        else
                        {
                            actividad.Id = 1;
                        }
                        actividad.CreatedAt = DateTime.Now;
                        actividad.PropertyId = id_propiedad;
                        actividad.Status = "active";
                        actividad.UpdatedAt = actividad.CreatedAt;

                        data.sumit(actividad);
                        if (data.bit_error) { throw data.Error; }
                    }
                    else
                    {
                        throw new Exception("No se pueden crear actividades, con fechas traslapadas");
                    }
                }
                else
                {
                    throw new Exception("La propieda no esta activa");
                }
            }
            catch (Exception ex)
            {
                bit_error = true;
                error = ex;
            }

        }


        public void reagendar_actividad(int id_actividad, DateTime fechanew)
        {
            B_Property neg_prop;
            entidades.Activity actividad;
            try
            {
                actividad = data.getSingle(f => f.Id == id_actividad);
                if (data.bit_error) { throw data.Error; }

                if (actividad.Status != "canceled")
                {
                    neg_prop = new B_Property();
                    if (neg_prop.propiedadActiva(actividad.PropertyId))
                    {
                        if (data.getList(f => f.PropertyId == actividad.PropertyId
                                          && f.Schedule >= fechanew.AddHours(-1)
                                          && f.Schedule <= fechanew.AddHours(1)
                                          && f.Status == "active").Count == 0)

                        {

                            actividad.UpdatedAt = DateTime.Now;
                            actividad.Schedule = fechanew;

                            data.sumit(actividad);
                            if (data.bit_error) { throw data.Error; }
                        }
                        else
                        {
                            throw new Exception("La fecha no esta disponible");
                        }
                    }
                    else
                    {
                        throw new Exception("La propieda no esta activa");
                    }
                }
                else
                {
                    throw new Exception("No se pueden re-agendar actividades canceladas");
                }
            }
            catch (Exception ex)
            {
                bit_error = true;
                error = ex;
            }

        }


        public List<object> GetList()
        {
            List<object> result;
            try
            {
                data.getList(s => s.Schedule <= DateTime.Now.AddDays(14) && s.Schedule >= DateTime.Now.AddDays(-3)).ToList();
                result = new List<object>();
            }
            catch (Exception ex)
            {
                bit_error = true;
                error = ex;
                result = new List<object>();
            }
            return result;
        }
    }
}
