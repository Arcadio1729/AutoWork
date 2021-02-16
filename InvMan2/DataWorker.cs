using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvMan2
{
    public class DataWorker<T>
    {
        private string sourcePath;
        public string SourcePath
        {
            get { return this.sourcePath; }
        }


        private string sourceContent;
        public string SourceContent
        {
            get { return this.sourceContent; }
        }


        private DataTable data;
        public DataTable Data
        {
            get { return this.data; }
        }

        public DataWorker(string sourcePath)
        {
            try
            {
                this.sourcePath = sourcePath;

                using (StreamReader sr = new StreamReader(this.SourcePath))
                {
                    this.sourceContent=sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public Object getDataJson()
        {
            return JsonSerializer.Deserialize<T>(this.SourceContent);
        }
    }
}
