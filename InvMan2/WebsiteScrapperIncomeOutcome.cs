using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DBM;
namespace InvMan
{
    public class WebsiteScrapperIncomeOutcome : HtmlWorker, IFixedValues
    {
        private HtmlAgilityPack.HtmlDocument htmlDocumentObject = new HtmlAgilityPack.HtmlDocument();

        private string htmlText;

        private List<BalancePosition> _Ebit;
        public List<BalancePosition> Ebit
        {
            get { return _Ebit; }
            set { _Ebit = value; }
        }

        private List<string> _periods;
        public List<string> Periods
        {
            get { return _periods; }
            set { _periods = value; }
        }

        public WebsiteScrapperIncomeOutcome(string webAddress) : base(webAddress)
        {
            htmlDocumentObject.LoadHtml(WebSiteSource);
            htmlText = "";
        }

        private List<BalancePosition> getBalancePosition(string positionName)
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

        public void loadEBIT()
        {
            Ebit = getBalancePosition("INCOMEEBIT");
        }

        public string getFixedValue(string valueName)
        {
            throw new NotImplementedException();
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
