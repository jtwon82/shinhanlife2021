using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeSummer.Business
{
    public class Dash : IDisposable
    {
        private Access.Dash _dash = null;

        public Dash(string connection)
        {
            _dash = new Access.Dash(connection);
        }

        public DataSet Summary()
        {
            return _dash.Summary();
        }

        public void Dispose()
        {
            _dash = null;
        }
    }
}
