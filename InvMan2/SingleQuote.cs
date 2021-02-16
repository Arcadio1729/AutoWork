using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvMan2
{
    public struct SingleQuote
    {
        public string StockFullName { get; set; }
        public string StockTicker { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal QuoteValue{get;set;}

    }
}
