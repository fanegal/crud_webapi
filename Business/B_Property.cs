﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_access;

namespace Business
{
    public class B_Property
    {

        private data_access.Property data;
        public bool bit_error;
        public Exception error;
        public B_Property()
        {
            data = new data_access.Property();
        }
        public void nuevo()
        {
            entidades.Property obj;
            try
            {
                obj = new entidades.Property();
                var indice = data.getAll().Count;
                if (indice > 0)
                {
                    obj.Id = indice + 1;
                }
                else
                {
                    obj.Id = 1;
                }
                obj.Address = "";
                obj.CreatedAt = DateTime.Now;
                obj.Description = "";
                obj.Status = "";
                obj.Title = "";
                obj.UpdatedAt = obj.CreatedAt;

                data.sumit(obj);
                if (data.bit_error) { throw data.Error; }
            }
            catch (Exception ex)
            {
                bit_error = true;
                error = ex;
            }

        }

        public bool propiedadActiva(int llave)
        {
            bool result = false;
            entidades.Property prop;
            try
            {

                prop = data.getSingle(f => f.Id == llave);
                if (data.bit_error) { throw data.Error; }

                if (prop.DisabledAt == null && prop.Id > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                bit_error = true;
                error = ex;
            }
            return result;
        }
    }
}
