using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.16
    /// 관리자 Model
    /// </summary>
    public class Admin
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkAdmin { get; set; }
        public string Usr { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public string Reset { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UseYn { get; set; }
        public string DelYn { get; set; }
        public string RegistDate { get; set; }

        public Admin Adm { get; set; }
    }
}