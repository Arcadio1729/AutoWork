using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBM;
using InvMan2;
using System.Data;
using System.Globalization;

namespace InvMan
{
    public class WebsiteScrapperQuotes : HtmlWorker, IFixedValues
    {
        private HtmlAgilityPack.HtmlDocument htmlDocumentObject = new HtmlAgilityPack.HtmlDocument();

        private string htmlText;

        private int myVar;

        private string errorPath = @"C:\Users\arcad\Desktop\ja\CS\DB\InvMan2\errors.txt";
        public List<StocksQuote> stocksQuotes { get; set; }

        public WebsiteScrapperQuotes(string webAddress) : base(webAddress)
        {
            this.htmlDocumentObject.LoadHtml(WebSiteSource);
            this.htmlText = "";
        }


        public List<BalancePosition> getBalancePosition(string positionName)
        {
            throw new NotImplementedException();
        }

        public string getFixedValue(string valueName)
        {
            throw new NotImplementedException();
        }

        public void getPeriods()
        {
            throw new NotImplementedException();
        }
        
        public void loadStocksQuotes(DateTime date)
        {
            this.stocksQuotes = this.getQuotes(date);
        }

        public List<StocksQuote> getQuotes(DateTime date)
        {
            HtmlAgilityPack.HtmlDocument htmlDocumentObject2 = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject;
            htmlNodeCollectionObject = htmlDocumentObject.DocumentNode.SelectNodes("//tbody/tr");

            if (htmlNodeCollectionObject == null) return null;

            var ci = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            ci.NumberFormat.NumberDecimalSeparator = ",";

            List<StocksQuote> quotes = new List<StocksQuote>();
            foreach(HtmlAgilityPack.HtmlNode node in htmlNodeCollectionObject)
            {
                try
                {
                    Console.WriteLine(node.InnerText);
                    var lst = node.Descendants("td").Select(td => td.InnerText.Trim()).ToList();
                    quotes.Add(new StocksQuote { FullName = lst[0], QuoteValue = decimal.Parse(lst[2],ci), QuoteDate = date });
                }
                catch(Exception e)
                {
                    Logger.logError(this.errorPath, "STOCKSQUOTES - " + e.ToString(), true);
                }
            }
            return quotes;

            //var tempColl = htmlNodeCollectionObject.Where((node) => (node.Attributes["data-field"] != null && node.Attributes["data-field"].Value.ToString().ToUpper() == positionName));

            //var temp = tempColl.FirstOrDefault();
            
            //htmlDocumentObject2.LoadHtml(temp.InnerHtml.ToString());

            //HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject2 = htmlDocumentObject2.DocumentNode.SelectNodes("//tr");

            //var tempColl2 = htmlNodeCollectionObject2.Where((node) => (node.Attributes["class"] == null && node.LastChild.Name != "span")).Select((node) => node.InnerText).ToList();

            //List<BalancePosition> CurrentBalancePosition = new List<BalancePosition>();

            //for (int i = 0; i < tempColl2.Count; i++)
            //{
            //    CurrentBalancePosition.Add(new BalancePosition(Periods[i], tempColl2[i]));
            //}

            //return CurrentBalancePosition;
        }

    }
}
