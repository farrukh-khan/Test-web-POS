using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISpService
    {
        int ExcuteSp(string query, SqlParameter[] Parameters);
        List<dynamic> ExcuteSpAnonmious(string query, SqlParameter[] Parameters);

        DataSet ExcuteSpAnonmious(string query, SqlParameter[] Parameters, int dataTableCount);

        

        
    }
}
