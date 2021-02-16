using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InvMan2
{
    public class Logger
    {
        public static void logError(string path,string errMessage,Boolean append)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(errMessage);
            }
        }
    }
}
