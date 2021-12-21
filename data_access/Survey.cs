using entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace data_access
{
    public class Survey
    {
        private conexion Oconexion;
        private postgresContext entities;
        public bool bit_error;
        public Exception Error;

        public Survey()
        {
            Oconexion = new conexion();
            entities = Oconexion.star_dbcore();
        }
    }
}
