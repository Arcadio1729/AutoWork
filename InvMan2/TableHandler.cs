using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using InvMan2;

namespace DBM
{
    public class TableHandler
    {
        private string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private DataTable _websiteData;
        public DataTable WebsiteData
        {
            get { return _websiteData; }
            private set { _websiteData = value; }
        }

        private IList<CurrentAsset> _currentAssetsFromWebsite;
        public IList<CurrentAsset> CurrentAssetsFromWebsite
        {
            get { return _currentAssetsFromWebsite; }
            set { _currentAssetsFromWebsite = value; }
        }

        private List<string> _currentAssetsFromDataBase;
        public List<string> CurrentAssetsFromDataBase
        {
            get { return _currentAssetsFromDataBase; }
            private set { _currentAssetsFromDataBase = value; }
        }


        private IList<Amortization> _amortizationFromWebsite;
        public IList<Amortization> AmortizationFromWebsite
        {
            get { return _amortizationFromWebsite; }
            set { _amortizationFromWebsite = value; }
        }

        private List<string> _amortizationFromDataBase;
        public List<string> AmortizationFromDataBase
        {
            get { return _amortizationFromDataBase; }
            private set { _amortizationFromDataBase = value; }
        }


        private IList<BalanceCapital> _balanceCapitalFromWebsite;
        public IList<BalanceCapital> BalanceCapitalFromWebsite
        {
            get { return _balanceCapitalFromWebsite; }
            set { _balanceCapitalFromWebsite = value; }
        }

        private List<string> _balanceCapitalFromDataBase;
        public List<string> BalanceCapitalFromDataBase
        {
            get { return _balanceCapitalFromDataBase; }
            private set { _balanceCapitalFromDataBase = value; }
        }


        private IList<CurrentLiability> _currentLiabilitiesFromWebsite;
        public IList<CurrentLiability> CurrentLiabilitiesFromWebsite
        {
            get { return _currentLiabilitiesFromWebsite; }
            set { _currentLiabilitiesFromWebsite = value; }
        }

        private List<string> _currentLiabilitiesFromDataBase;
        public List<string> CurrentLiabilitiesFromDataBase
        {
            get { return _currentLiabilitiesFromDataBase; }
            private set { _currentLiabilitiesFromDataBase = value; }
        }

        private IList<Ebit> _ebitFromWebsite;
        public IList<Ebit> EbitFromWebsite
        {
            get { return _ebitFromWebsite; }
            set { _ebitFromWebsite = value; }
        }

        private IList<StocksQuote> _stocksQuotesFromWebsite;
        public IList<StocksQuote> StocksQuotesFromWebsite
        {
            get { return _stocksQuotesFromWebsite; }
            set { _stocksQuotesFromWebsite = value; }
        }

        private List<string> _ebitFromDataBase;
        public List<string> EbitFromDataBase
        {
            get { return _ebitFromDataBase; }
            private set { _ebitFromDataBase = value; }
        }



        private IList<TotalAsset> _totalAssetsFromWebsite;
        public IList<TotalAsset> TotalAssetsFromWebsite
        {
            get { return _totalAssetsFromWebsite; }
            set { _totalAssetsFromWebsite = value; }
        }

        private List<string> _totalAssetsFromDataBase;
        public List<string> TotalAssetsFromDataBase
        {
            get { return _totalAssetsFromDataBase; }
            private set { _totalAssetsFromDataBase = value; }
        }



        private StocksDetail _stocksDetailsFromWebsi;
        public StocksDetail StocksDetailsFromWebsite
        {
            get { return _stocksDetailsFromWebsi; }
            set { _stocksDetailsFromWebsi = value; }
        }

        public TableHandler(string tn)
        {
            this.TableName = tn;
        }

        public void updateData()
        {
            InvestDB2Entities context = new InvestDB2Entities();
            switch (this.TableName.ToUpper())
            {
                case "BALANCECAPITAL":
                    context.BalanceCapitals.AddRange((IEnumerable<BalanceCapital>)this.BalanceCapitalFromWebsite);
                    break;

                case "CURRENTLIABILITIES":
                    context.CurrentLiabilities.AddRange((IEnumerable<CurrentLiability>)this.CurrentLiabilitiesFromWebsite);
                    break;

                case "AMORTIZATION":
                    context.Amortizations.AddRange((IEnumerable<Amortization>)this.AmortizationFromWebsite);
                    break;

                case "CURRENTASSETS":
                    context.CurrentAssets.AddRange((IEnumerable<CurrentAsset>)this.CurrentAssetsFromWebsite);
                    break;

                case "TOTALASSETS":
                    context.TotalAssets.AddRange((IEnumerable<TotalAsset>)this.TotalAssetsFromWebsite);
                    break;

                case "EBIT":
                    context.Ebits.AddRange((IEnumerable<Ebit>)this.EbitFromWebsite);
                    break;

                case "STOCKSDETAILS":
                    context.StocksDetails.Add(this.StocksDetailsFromWebsite);
                    break;

                case "STOCKSQUOTES":
                    context.StocksQuotes.AddRange((IEnumerable<StocksQuote>)this.StocksQuotesFromWebsite);
                    break;
            }
            context.SaveChanges();
        }
    
        public void getDataFromDataBase()
        {
            InvestDB2Entities context = new InvestDB2Entities();
            System.Data.Entity.DbSet dataFromDb;
            switch (this.TableName.ToUpper())
            {
                case "AMORTIZATION":
                    dataFromDb = context.Amortizations;
                    this.AmortizationFromDataBase = new List<string>();
                    foreach (Amortization item in dataFromDb)
                    {
                        this.AmortizationFromDataBase.Add(item.Name_ID);
                    }
                    break;


                case "TOTALASSETS":
                    dataFromDb = context.TotalAssets;
                    this.TotalAssetsFromDataBase = new List<string>();
                    foreach (TotalAsset item in dataFromDb)
                    {
                        this.TotalAssetsFromDataBase.Add(item.Name_ID);
                    }
                    break;

                case "CURRENTASSETS":
                    dataFromDb = context.CurrentAssets;
                    this.CurrentAssetsFromDataBase = new List<string>();
                    foreach (CurrentAsset item in dataFromDb)
                    {
                        this.CurrentAssetsFromDataBase.Add(item.Name_ID);
                    }
                    break;

                case "BALANCECAPITAL":
                    dataFromDb = context.BalanceCapitals;
                    this.BalanceCapitalFromDataBase = new List<string>();
                    foreach (BalanceCapital item in dataFromDb)
                    {
                        this.BalanceCapitalFromDataBase.Add(item.Name_ID);
                    }
                    break;

                case "CURRENTLIABILITIES":
                    dataFromDb = context.CurrentLiabilities;
                    this.CurrentLiabilitiesFromDataBase = new List<string>();
                    foreach (CurrentLiability item in dataFromDb)
                    {
                        this.CurrentLiabilitiesFromDataBase.Add(item.Name_ID);
                    }
                    break;

                case "EBIT":
                    dataFromDb = context.Ebits;
                    this.EbitFromDataBase = new List<string>();
                    foreach (Ebit item in dataFromDb)
                    {
                        this.EbitFromDataBase.Add(item.Name_ID);
                    }
                    break;
            }

        }
        
        public Boolean nameIdExists(string nameId)
        {
            switch (this.TableName.ToUpper())
            {
                case "AMORTIZATION":
                    if (this.AmortizationFromDataBase.Contains(nameId)) return true;
                    return false;
                case "CURRENTASSETS":
                    if (this.CurrentAssetsFromDataBase.Contains(nameId)) return true;
                    return false;
                case "CURRENTLIABILITIES":
                    if (this.CurrentLiabilitiesFromDataBase.Contains(nameId)) return true;
                    return false;
                case "BALANCECAPITAL":
                    if (this.BalanceCapitalFromDataBase.Contains(nameId)) return true;
                    return false;
                case "TOTALASSETS":
                    if (this.TotalAssetsFromDataBase.Contains(nameId)) return true;
                    return false;
                case "EBIT":
                    if (this.EbitFromDataBase.Contains(nameId)) return true;
                    return false;
            }

            return false;
        }

        public Boolean canConnect()
        {
            var context = new InvestDB2Entities();
            
            if (context.Database.Exists())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void loadDataFromWebsiteFormat(List<BalancePosition> data, string ticker)
        {
            string idName="";

            switch (this.TableName.ToUpper())
            {
                case "AMORTIZATION":
                    this.AmortizationFromWebsite = new List<Amortization>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.AmortizationFromWebsite.Add(new Amortization() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;

                case "CURRENTASSETS":
                    this.CurrentAssetsFromWebsite = new List<CurrentAsset>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.CurrentAssetsFromWebsite.Add(new CurrentAsset() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;

                case "CURRENTLIABILITIES":
                    this.CurrentLiabilitiesFromWebsite = new List<CurrentLiability>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.CurrentLiabilitiesFromWebsite.Add(new CurrentLiability() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;

                case "EBIT":
                    this.EbitFromWebsite = new List<Ebit>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.EbitFromWebsite.Add(new Ebit() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;

                case "TOTALASSETS":
                    this.TotalAssetsFromWebsite = new List<TotalAsset>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.TotalAssetsFromWebsite.Add(new TotalAsset() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;
                case "BALANCECAPITAL":
                    this.BalanceCapitalFromWebsite = new List<BalanceCapital>();
                    foreach (BalancePosition a in data)
                    {
                        idName = ticker + "_" + a.period;
                        if (!this.nameIdExists(idName))
                        {
                            this.BalanceCapitalFromWebsite.Add(new BalanceCapital() { Name_ID = idName, Ticker = ticker, Period = a.period, Amount = Convert.ToDecimal(a.value.Replace(" ", "")) });
                        }
                    }
                    break;
            }
        }

        public void loadDataFromWebsiteFormat(int data, string ticker)
        {
            switch (this.TableName.ToUpper())
            {
                case "STOCKSDETAILS":
                    this.StocksDetailsFromWebsite = new StocksDetail();
                    this.StocksDetailsFromWebsite = new StocksDetail() { Ticker = ticker, Name = ticker, StocksAmount = data };
                    break;
            }
        }

        public void loadDataFromWebsiteFormat(List<StocksQuote> data)
        {
            switch (this.TableName.ToUpper())
            {
                case "STOCKSQUOTES":
                    this.StocksQuotesFromWebsite = new List<StocksQuote>();
                    this.StocksQuotesFromWebsite = data;
                    break;
            }
        }
    }
}
