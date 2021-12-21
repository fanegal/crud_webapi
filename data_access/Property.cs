using entidades; 
using System;
using System.Collections.Generic;
using System.Linq; 

namespace data_access
{
    public class Property
    {

        private conexion Oconexion;
        private postgresContext entities;
        public bool bit_error;
        public Exception Error;

        public Property()
        {
            Oconexion = new conexion();
            entities = Oconexion.star_dbcore();
        }



        public List<entidades.Property> getAll()
        {
            List<entidades.Property> List_result = new List<entidades.Property>();
            try
            {
                List_result = entities.Properties.ToList();
            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
            }

            return List_result;
        }

        public List<entidades.Property> getList(System.Linq.Expressions.Expression<Func<entidades.Property, bool>> condicion)
        {
            List<entidades.Property> List_result = new List<entidades.Property>();
            try
            {
                List_result = entities.Properties.Where(condicion).ToList();


            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
            }

            return List_result;
        }

        public entidades.Property getSingle(System.Linq.Expressions.Expression<Func<entidades.Property, bool>> condicion)
        {
            entidades.Property result = new entidades.Property();
            try
            {
                result = entities.Properties.Where(condicion).Single();
            }
            catch (Exception ex)
            {
                bit_error = true;
                Error = ex;
                result = new entidades.Property();
            }

            return result;
        }

        public void sumit(entidades.Property objeto)
        {
            try
            {
                entidades.Property oldobjeto = entities.Properties.Where(t => t.Id == objeto.Id).FirstOrDefault();
                if (oldobjeto == null)
                {
                    entities.Properties.Add(objeto);
                }
                else
                {
                    var props = objeto.GetType().GetProperties();

                    foreach (System.Reflection.PropertyInfo prop in props.ToList())
                    {
                        if (prop.Name.ToLower() != "Activity")
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
