using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.DataBase;

namespace OrangeSummer.Access
{
    public class Dash
    {
        private string _connection = string.Empty;

        public Dash(string connection)
        {
            _connection = connection;
        }

        public DataSet Summary()
        {
            return DBHelper.ExecuteDataSet(_connection, "ADM_DASH_LIST");
        }
    }
}
