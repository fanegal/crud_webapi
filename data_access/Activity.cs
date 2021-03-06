using entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace data_access
{
    public class Activity
    {
        private conexion Oconexion;
        private postgresContext entities;
        public bool bit_error;
        public Exception Error;

        public Activity()
        {
            Oconexion = new conexion();
            entities = Oconexion.star_dbcore();
        }

        public List<entidades.Activity> getAll()
        {
            List<entidades.Activity> List_result = new List<entidades.Activity>();
            try
            {
                List_result = entities.Activities.ToList();
            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
            }

            return List_result;
        }

        public IEnumerable<lista> getListEspecial_regla()
        {
            IEnumerable<lista> List_result;
            try
            {
                List_result = from t1 in entities.Activities
                              join t2 in entities.Surveys
                              on t1.Id equals t2.ActivityId
                              into temp
                              from t2 in temp.DefaultIfEmpty()
                              where t1.Schedule <= DateTime.Now.AddDays(14) && t1.Schedule >= DateTime.Now.AddDays(-3)
                              select new lista
                              {
                                  ID = t1.Id,
                                  schedule = t1.Schedule,
                                  title = t1.Title,
                                  created_at = t1.CreatedAt,
                                  status = t1.Status,
                                  condition = t1.Status == "active" && t1.Schedule >= DateTime.Now ? "Pendiente a realizar"
                                                : t1.Status == "active" && t1.Schedule < DateTime.Now ? "Atrasada"
                                                : t1.Status == "done" ? "Finalizada" : "",
                                  property = new propiedad
                                  {
                                      id = t1.Property.Id,
                                      title = t1.Property.Title,
                                      address = t1.Property.Address
                                  },
                                  survey = t2.Id.ToString()
                              };
                return List_result;

            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
                List_result = new List<lista>();
                return List_result;
            }
        }

        public IEnumerable<lista> getListEspecial_status(string estatus)
        {
            IEnumerable<lista> List_result;
            try
            {
                List_result = from t1 in entities.Activities
                              join t2 in entities.Surveys
                              on t1.Id equals t2.ActivityId
                              into temp
                              from t2 in temp.DefaultIfEmpty()
                              where t1.Status.ToLower().Equals(estatus.ToLower())
                              select new lista
                              {
                                  ID = t1.Id,
                                  schedule = t1.Schedule,
                                  title = t1.Title,
                                  created_at = t1.CreatedAt,
                                  status = t1.Status,
                                  condition = t1.Status == "active" && t1.Schedule >= DateTime.Now ? "Pendiente a realizar"
                                                 : t1.Status == "active" && t1.Schedule < DateTime.Now ? "Atrasada"
                                                 : t1.Status == "done" ? "Finalizada" : "",
                                  property = new propiedad
                                  {
                                      id = t1.Property.Id,
                                      title = t1.Property.Title,
                                      address = t1.Property.Address
                                  },
                                  survey = t2.Id.ToString()
                              };

                return List_result;

            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
                List_result = new List<lista>();
                return List_result;
            }
        }

        public IEnumerable<lista> getListEspecial_fechas(DateTime inicio, DateTime fin)
        {
            IEnumerable<lista> List_result;
            try
            {
                List_result = from t1 in entities.Activities
                              join t2 in entities.Surveys
                              on t1.Id equals t2.ActivityId
                              into temp
                              from t2 in temp.DefaultIfEmpty()
                              where t1.Schedule <= fin && t1.Schedule >= inicio
                              select new lista
                              {
                                  ID = t1.Id,
                                  schedule = t1.Schedule,
                                  title = t1.Title,
                                  created_at = t1.CreatedAt,
                                  status = t1.Status,
                                  condition = t1.Status == "active" && t1.Schedule >= DateTime.Now ? "Pendiente a realizar"
                                                  : t1.Status == "active" && t1.Schedule < DateTime.Now ? "Atrasada"
                                                  : t1.Status == "done" ? "Finalizada" : "",
                                  property = new propiedad
                                  {
                                      id = t1.Property.Id,
                                      title = t1.Property.Title,
                                      address = t1.Property.Address
                                  },
                                  survey = t2.Id.ToString()
                              };
                return List_result;

            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
                List_result = new List<lista>();
                return List_result;
            }
        }
        public List<entidades.Activity> getList(System.Linq.Expressions.Expression<Func<entidades.Activity, bool>> condicion)
        {
            List<entidades.Activity> List_result = new List<entidades.Activity>();
            try
            {
                List_result = entities.Activities.Where(condicion).ToList();


            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
            }

            return List_result;
        }

        public entidades.Activity getSingle(System.Linq.Expressions.Expression<Func<entidades.Activity, bool>> condicion)
        {
            entidades.Activity result = new entidades.Activity();
            try
            {
                result = entities.Activities.Where(condicion).Single();
            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
                result = new entidades.Activity();
            }

            return result;
        }

        public void sumit(entidades.Activity objeto)
        {
            try
            {
                entidades.Activity oldobjeto = entities.Activities.Where(t => t.Id == objeto.Id).FirstOrDefault();
                if (oldobjeto == null)
                {
                    entities.Activities.Add(objeto);
                }
                else
                {
                    var props = objeto.GetType().GetProperties();

                    foreach (System.Reflection.PropertyInfo prop in props.ToList())
                    {
                        if (prop.Name.ToLower() != "Property")
                            prop.SetValue(oldobjeto, prop.GetValue(objeto));
                    }

                }

                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;

                throw;
            }

        }

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
        public int id { get; set; }
        public string title { get; set; }
        public string address { get; set; }
    }
}
