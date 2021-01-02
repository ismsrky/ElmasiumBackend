using Dapper;
using Mh.Dto.Module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Module
{
    public class ModuleBusiness
    {
        public static List<ModuleDto> GetList()
        {
            List<ModuleDto> list = null;

            string sql = @"select Id,[Name],Icon from tModule";

            using (SqlConnection conn = DbAccess.Connection.GetConn())
            {
                list = conn.Query<ModuleDto>(sql).ToList();
            }
            //sdfsf

            return list;
        }
    }
}