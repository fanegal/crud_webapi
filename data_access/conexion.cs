using entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class conexion
    {
        postgresContext conn;
        public conexion()
        { }

        public postgresContext star_dbcore()
        {
            conn = new postgresContext();
            return conn;
        }

    }
}
