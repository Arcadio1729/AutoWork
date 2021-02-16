using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;
using DBM;
namespace InvMan
{
    public class WebsiteScrapperBalanceSheet : HtmlWorker, IFixedValues
    {
        private HtmlAgilityPack.HtmlDocument htmlDocumentObject = new HtmlAgilityPack.HtmlDocument();
        //private HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject;

        private string htmlText;

        private List<BalancePosition> _currentAssets;
        public List<BalancePosition> CurrentAssets
        {
            get { return _currentAssets; }
            private set { _currentAssets = value; }
        }

        private List<BalancePosition> _balancePositions;
        public List<BalancePosition> BalanceCapital
        {
            get { return _balancePositions; }
            set { _balancePositions = value; }
        }

        private List<BalancePosition> _balanceCurrentLiabilities;
        public List<BalancePosition> BalanceCurrentLiabilities
        {
            get { return _balanceCurrentLiabilities; }
            set { _balanceCurrentLiabilities = value; }
        }

        private List<BalancePosition> _balanceTotalAssets;
        public List<BalancePosition> BalanceTotalAssets
        {
            get { return _balanceTotalAssets; }
            set { _balanceTotalAssets = value; }
        }


        private List<string> _periods;
        public List<string> Periods
        {
            get { return _periods; }
            set { _periods = value; }
        }

        private int _stocksAmount;

        public int StocksAmount
        {
            get { return _stocksAmount; }
            set { _stocksAmount = value; }
        }

        public WebsiteScrapperBalanceSheet(string webAddress) : base(webAddress)
        {
            htmlDocumentObject.LoadHtml(WebSiteSource);
            htmlText = "";

            this.BalanceTotalAssets = new List<BalancePosition>();
        }

        public void getPeriods()
        {
            //htmlNodeCollectionObject = htmlDocumentObject.DocumentNode.SelectNodes("//th");
            htmlText = htmlDocumentObject.Text;

            Regex rx = new Regex(@"\d{4}\/Q\d{1}");
            MatchCollection matches = rx.Matches(htmlText);

            Periods = new List<string>();
            foreach(Match m in matches)
            {
                //Console.WriteLine(m.Groups[0].ToString().Replace(@"/","-"));
                Periods.Add(m.Value.ToString().Replace(@"/", "-"));
            }
           
        }
        string getFixedValue(string valueName)
        {
            HtmlAgilityPack.HtmlDocument htmlDocumentObject2 = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNodeCollection htmlNodeCollectionObject;
            htmlNodeCollectionObject = htmlDocumentObject.DocumentNode.SelectNodes("//div/table/tr");

            string outputResult = "";

            foreach (HtmlAgilityPack.HtmlNode d in htmlNodeCollectionObject)
            {

                if(d.InnerHtml.ToString().Contains(valueName))
                {

                    outputResult = d.ChildNodes.Where((node)=>(node.Name.ToString()=="td")).FirstOrDefault().InnerText.Trim();
                }
            }

            return outputResult;
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
        public void loadCurrentAssets()
        {
            CurrentAssets = new List<BalancePosition>();
            CurrentAssets = getBalancePosition("BALANCECURRENTASSETS");
        } 
        public void loadBalanceCapital()
        {
            BalanceCapital = new List<BalancePosition>();
            BalanceCapital = getBalancePosition("BALANCECAPITAL");
        }
        public void loadBalanceCurrentLiabilities()
        {
            BalanceCurrentLiabilities = new List<BalancePosition>();
            BalanceCurrentLiabilities = getBalancePosition("BALANCECURRENTLIABILITIES");
        }
        public void loadBalanceTotalAssets()
        {
            BalanceTotalAssets = new List<BalancePosition>();
            BalanceTotalAssets = getBalancePosition("BALANCETOTALASSETS");
        }
        public void loadStocksAmount()
        {
            StocksAmount = Int32.Parse(getFixedValue("Liczba akcji:").Replace(" ",""));
        }

        void IFixedValues.getPeriods()
        {
            throw new NotImplementedException();
        }

        List<BalancePosition> IFixedValues.getBalancePosition(string positionName)
        {
            throw new NotImplementedException();
        }

        string IFixedValues.getFixedValue(string valueName)
        {
            throw new NotImplementedException();
        }
    }


}
