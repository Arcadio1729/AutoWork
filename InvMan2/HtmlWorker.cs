using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace InvMan
{
    public class HtmlWorker
    {
        private HttpClient _httpClient;

        private string _webAddress;
        public string WebAddress
        {
            get { return _webAddress; }
            set { _webAddress = value; }
        }


        private string _webSiteSource;
        public string WebSiteSource
        {
            get { return _webSiteSource; }
            set { _webSiteSource = value; }
        }

        public HtmlWorker(string webAddress)
        {
            this.WebAddress = webAddress;
            _httpClient = new HttpClient();
            scrapSourceCodeTool();
        }

        private void scrapSourceCodeTool()
        {
            try
            {
                WebSiteSource = _httpClient.GetStringAsync(WebAddress).Result;
            }
            catch (InvalidCastException e)
            {
                throw (new Exception(e.ToString()));
            }
        }
    }
}
