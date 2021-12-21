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
}
