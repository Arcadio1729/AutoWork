using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvMan;
using DBM;

namespace InvMan2
{
    public class GpwTickers
    {
        public List<Ticker> GPW { get; set; }
    }
    public class Ticker
    {
        public string ticker { get; set; }
    }

    public class AUTOGRABBER
    {
        private string BalanceSheetURL = @"https://www.biznesradar.pl/raporty-finansowe-bilans/XXX,Q,0";
        private string CashFlowURL = @"https://www.biznesradar.pl/raporty-finansowe-przeplywy-pieniezne/XXX,Q";
        private string IncomeOutcomeURL = "https://www.biznesradar.pl/raporty-finansowe-rachunek-zyskow-i-strat/XXX,Q";

        private string StocksQuotesURL = @"https://www.gpw.pl/archiwum-notowan?fetch=0&type=10&instrument=&date=XXX&show_x=Poka%C5%BC+wyniki";



        public List<Ticker> Tickers { get; set; }

        private string tickerPath;
        private string errorPath;
        public string TickerPath
        {
            get { return tickerPath; }
            private set { tickerPath = value; }
        }

        public AUTOGRABBER(string tickerPath,string errorPath)
        {
            this.tickerPath = tickerPath;
            this.errorPath = errorPath;

            GpwTickers tempData;
            DataWorker<GpwTickers> dw = new DataWorker<GpwTickers>(this.TickerPath);

            tempData = (GpwTickers)dw.getDataJson();
            this.Tickers=(List<Ticker>)tempData.GPW;
        }


        public void updateBalanceSheetPosition(string positionName,params string[] args)
        {
            string CurrentBalanceSheetURL = "";
            string CurrentCashFlowURL = "";
            string CurrentIncomeOutcomeURL = "";

            TableHandler th;

            switch (positionName.ToUpper())
            {
                case "BALANCECAPITAL":
                    th = new TableHandler("BalanceCapital");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperBalanceSheet wsbs = new WebsiteScrapperBalanceSheet(CurrentBalanceSheetURL);
                            wsbs.getPeriods();
                            wsbs.loadBalanceCapital();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wsbs.BalanceCapital, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, "" + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;

                case "CURRENTLIABILITIES":
                    th = new TableHandler("CurrentLiabilities");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperBalanceSheet wsbs = new WebsiteScrapperBalanceSheet(CurrentBalanceSheetURL);
                            wsbs.getPeriods();
                            wsbs.loadBalanceCurrentLiabilities();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wsbs.BalanceCurrentLiabilities, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, "" + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;

                case "CURRENTASSETS":
                    th = new TableHandler("CurrentAssets");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperBalanceSheet wsbs = new WebsiteScrapperBalanceSheet(CurrentBalanceSheetURL);
                            wsbs.getPeriods();
                            wsbs.loadCurrentAssets();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wsbs.CurrentAssets, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, positionName + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;

                case "TOTALASSETS":
                    th = new TableHandler("TotalAssets");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperBalanceSheet wsbs = new WebsiteScrapperBalanceSheet(CurrentBalanceSheetURL);
                            wsbs.getPeriods();
                            wsbs.loadBalanceTotalAssets();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wsbs.BalanceTotalAssets, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, positionName + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;
            }

        }    

        public void updateStocksQuotes(params string[] args)
        {
            string latestDate = args[1];
            string startDate = args[0];

            DateTime latestDateDt = Convert.ToDateTime(latestDate);
            DateTime startDateDt = Convert.ToDateTime(startDate);
            DateTime currentDateDt = latestDateDt;
            string currentDate = latestDate;
            while (currentDateDt > startDateDt)
            {
                TableHandler th = new TableHandler("StocksQuotes");

                try
                {
                    List<StocksQuote> currentStocksQuotes= null;

                    DateTime quotesDate = Convert.ToDateTime(currentDate);
                    string formattedQuotesDate = quotesDate.ToString("dd-MM-yyyy");
                    string currentURL = this.StocksQuotesURL.Replace("XXX", formattedQuotesDate);
                    WebsiteScrapperQuotes wsq = new WebsiteScrapperQuotes(currentURL);
                    while (currentStocksQuotes == null)
                    {
                        wsq.loadStocksQuotes(Convert.ToDateTime(quotesDate));
                        currentStocksQuotes = wsq.stocksQuotes;
                        if (currentStocksQuotes == null)
                        {
                            currentDateDt = currentDateDt.AddDays(-1);
                            currentDate=currentDateDt.ToString("dd-MM-yyyy");
                        }
                    }

                    th.loadDataFromWebsiteFormat(wsq.stocksQuotes);
                    th.updateData();

                    currentDateDt = currentDateDt.AddMonths(-3);
                    currentDate = currentDateDt.ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Logger.logError(this.errorPath, "STOCKSQUOTES - " + e.ToString(), true);
                }
            }
        }
        public void updateCashFlowPosition(string positionName)
        {
            string CurrentBalanceSheetURL = "";
            string CurrentCashFlowURL = "";
            string CurrentIncomeOutcomeURL = "";

            TableHandler th;

            switch (positionName.ToUpper())
            {
                case "AMORTIZATION":
                    th = new TableHandler("Amortization");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperCashFlow wscf = new WebsiteScrapperCashFlow(CurrentCashFlowURL);
                            wscf.getPeriods();
                            wscf.loadAmortization();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wscf.AmortizationFS, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, positionName + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;
            }

        }
        public void updateIncomeOutcomePosition(string positionName)
        {
            string CurrentBalanceSheetURL = "";
            string CurrentCashFlowURL = "";
            string CurrentIncomeOutcomeURL = "";

            TableHandler th;

            switch (positionName.ToUpper())
            {
                case "EBIT":
                    th = new TableHandler("Ebit");

                    foreach (Ticker t in this.Tickers)
                    {
                        CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                        CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                        CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                        try
                        {
                            WebsiteScrapperIncomeOutcome wsio = new WebsiteScrapperIncomeOutcome(CurrentIncomeOutcomeURL);
                            wsio.getPeriods();
                            wsio.loadEBIT();

                            th.getDataFromDataBase();
                            th.loadDataFromWebsiteFormat(wsio.Ebit, t.ticker);
                            th.updateData();

                            Console.WriteLine(t.ticker);
                        }
                        catch (Exception e)
                        {
                            Logger.logError(this.errorPath, positionName + t.ticker + " - " + e.ToString(), true);
                        }
                    }
                    break;
            }

        }

        public void updateStockDetails()
        {
            string CurrentBalanceSheetURL = "";
            string CurrentCashFlowURL = "";
            string CurrentIncomeOutcomeURL = "";
            TableHandler th;
            foreach (Ticker t in this.Tickers)
            {
                th = new TableHandler("STOCKSDETAILS");
                CurrentBalanceSheetURL = BalanceSheetURL.Replace("XXX", t.ticker);
                CurrentCashFlowURL = CashFlowURL.Replace("XXX", t.ticker);
                CurrentIncomeOutcomeURL = IncomeOutcomeURL.Replace("XXX", t.ticker);

                try
                {
                    WebsiteScrapperBalanceSheet wsbs = new WebsiteScrapperBalanceSheet(CurrentBalanceSheetURL);

                    wsbs.loadStocksAmount();
                    th.loadDataFromWebsiteFormat(wsbs.StocksAmount,t.ticker);
                    th.updateData();

                    Console.WriteLine(t.ticker);
                }
                catch (Exception e)
                {
                    Logger.logError(this.errorPath, "StocksAmount" + t.ticker + " - " + e.ToString(), true);
                }
            }

        }
    }
}
