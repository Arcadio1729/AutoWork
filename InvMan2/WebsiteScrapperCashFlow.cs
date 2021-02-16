using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using DBM;

namespace InvMan
{
    public class WebsiteScrapperCashFlow : HtmlWorker, IFixedValues
    {
        private HtmlAgilityPack.HtmlDocument htmlDocumentObject = new HtmlAgilityPack.HtmlDocument();

        private string htmlText;

        private List<string> _periods;
        public List<string> Periods
        {
            get { return _periods; }
            set { _periods = value; }
        }

        private List<BalancePosition> _amortizationfs;
        public List<BalancePosition> AmortizationFS
        {
            get { return _amortizationfs; }
            private set { _amortizationfs = value; }
        }

        public WebsiteScrapperCashFlow(string webAddress) : base(webAddress)
        {
            htmlDocumentObject.LoadHtml(WebSiteSource);
            htmlText = "";
        }

        List<BalancePosition> getBalancePosition(string positionName)
        {
            HtmlAgilityPack.HtmlDocument htmlDocumentObject2 = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject;
            htmlNodeCollectionObject = htmlDocumentObject.DocumentNode.SelectNodes("//tr");

            var tempColl = htmlNodeCollectionObject.Where((node) => (node.Attributes["data-field"] != null && node.Attributes["data-field"].Value.ToString().ToUpper() == positionName));

            var temp = tempColl.FirstOrDefault();

            htmlDocumentObject2.LoadHtml(temp.InnerHtml.ToString());

            HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject2 = htmlDocumentObject2.DocumentNode.SelectNodes("//span/span/span");

            var tempColl2 = htmlNodeCollectionObject2.Where((node) => (node.Attributes["class"] == null && node.LastChild.Name != "span")).Select((node) => node.InnerText).ToList();

            List<BalancePosition> CurrentBalancePosition = new List<BalancePosition>();

            for (int i = 0; i < tempColl2.Count; i++)
            {
                CurrentBalancePosition.Add(new BalancePosition(Periods[i], tempColl2[i]));
            }

            return CurrentBalancePosition;
        }

        public string getFixedValue(string valueName)
        {
            throw new NotImplementedException();
        }

        public void loadAmortization()
        {
            AmortizationFS= getBalancePosition("CASHFLOWAMORTIZATION");
        }

        public void getPeriods()
        {
            htmlText = htmlDocumentObject.Text;

            Regex rx = new Regex(@"\d{4}\/Q\d{1}");
            MatchCollection matches = rx.Matches(htmlText);

                Periods = new List<string>();
                foreach (Match m in matches)
                {
                    Periods.Add(m.Value.ToString().Replace(@"/", "-"));
                }
            
        }

        List<BalancePosition> IFixedValues.getBalancePosition(string positionName)
        {
            throw new NotImplementedException();
        }
    }
}
