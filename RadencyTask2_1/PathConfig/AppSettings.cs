using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.PathConfig
{
    public class AppSettings
    {
        public static string Key = "PathToFolder";
        public string ToRead { get; set; }
        public string ToWrite { get; set; }
    }
}
