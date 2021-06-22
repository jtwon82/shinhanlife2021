using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib.Attach
{
    public class Src
    {
        private string _path;

        public Src(string path)
        {
            _path = path;
        }

        public string Url()
        {
            string rtn;
            FileInfo info = new FileInfo(_path);
            if (info.Exists)
            {
                using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);
                        rtn = "data:image/jpeg;base64," + base64;
                    }
                }
            }
            else
            {
                rtn = "";
            }

            return rtn;
        }
    }
}
