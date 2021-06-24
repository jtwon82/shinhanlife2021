using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeSummer.Model
{
    public class Measure
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public string Gubun { get; set; }
        public string Title { get; set; }
        public string attMobile { get; set; }
        public string Contents { get; set; }
        public string sdate { get; set; }
        public string edate { get; set; }
        public string UseYn { get; set; }
        public string HitCnt { get; set; }
        public string RegistDate { get; set; }
        public Admin Admin { get; set; }
        public Branch Branch { get; set; }

        public string empty { get; set; }

    }
}
