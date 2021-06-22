using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib.Network
{
    public class Parameters
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public FileInfo FileInfo { get; set; }
    }

    public class FileInfo
    {
        public string ContentType { get; set; }
        public string Path { get; set; }
        public System.IO.Stream Stream { get; set; }
    }
}
