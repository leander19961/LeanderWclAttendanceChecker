using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.API.Web
{
    public class WebAPI
    {
        HttpClient client = new HttpClient();
        string token = "d6b9dc835f34b76475bc55964f037d60";

        public WebAPI()
        {
            this.client.BaseAddress = new Uri("https://www.warcraftlogs.com/v1");
        }

        public string GETLogList(string startTime, string endTime)
        {
            return client.GetStringAsync($"/reports/guild/Added/Antonidas/EU?api_key={token}").Result;
        }

        public string GETReport(string reportCode)
        {
            return client.GetStringAsync($"/report/fights/{reportCode}?api_key={token}").Result;
        }
    }
}
