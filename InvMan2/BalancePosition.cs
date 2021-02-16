using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBM
{
    public struct BalancePosition
    {
        public BalancePosition(string p, string v)
        {
            this.period = p;
            this.value = v;
        }
        public string period { get; set; }
        public string value { get; set; }
    }

}
