using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MLib.Attach
{
    public class CSV
    {
        private string _path = string.Empty;
        public CSV(string path)
        {
            _path = path;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = null;
            try
            {
                if (System.IO.File.Exists(_path))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(_path, Encoding.GetEncoding("euc-kr")))
                    {
                        dt = new DataTable();
                        Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        string[] headers = regex.Split(sr.ReadLine());
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dt.Columns.Add("F" + (i + 1).ToString());
                        }

                        using (System.IO.StreamReader reader = new System.IO.StreamReader(_path, Encoding.GetEncoding("euc-kr")))
                        {
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                string[] rows = regex.Split(line);

                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    dr[i] = rows[i];
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return dt;
        }
    }
}