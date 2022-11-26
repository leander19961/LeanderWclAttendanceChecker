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
        HttpClient client;
        string token = "d6b9dc835f34b76475bc55964f037d60";

        public WebAPI()
        {
            client = new HttpClient();
        }

        public string GETLogList()
        {
            return client.GetStringAsync($"https://www.warcraftlogs.com/v1/reports/guild/Added/Antonidas/EU?api_key={token}").Result;
        }

        public string GETLogList(string time, bool identifier)
        {
            if (identifier)
            {
                return client.GetStringAsync($"https://www.warcraftlogs.com/v1/reports/guild/Added/Antonidas/EU?api_key={token}").Result;
            }
            else
            {
                return client.GetStringAsync($"https://www.warcraftlogs.com/v1/reports/guild/Added/Antonidas/EU?api_key={token}").Result;
            }
        }

        public string GETLogList(string startTime, string endTime)
        {
            return client.GetStringAsync($"https://www.warcraftlogs.com/v1/reports/guild/Added/Antonidas/EU?api_key={token}").Result;
        }

        public string GETReport(string reportCode)
        {
            return client.GetStringAsync($"https://www.warcraftlogs.com/v1/report/fights/{reportCode}?api_key={token}").Result;
        }
    }
}
