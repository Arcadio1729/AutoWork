using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvMan2
{
    using System;

    public class DataBaseModel
    {
        public string Name_ID { get; set; }
        public string Ticker { get; set; }
        public string Period { get; set; }
        public Nullable<decimal> Amount { get; set; }

    }
}
