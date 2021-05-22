using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avansight.Domain.Services
{
    public class Repository<Entity>
    {
        public void Query<Entity>(string spName, object dyn, SqlConnection conn, ref List<Entity> rtn)
        {
            rtn = new DataAccessService().Query<Entity>(spName, dyn, CommandType.StoredProcedure, conn).ToList();
        }

    }
}
