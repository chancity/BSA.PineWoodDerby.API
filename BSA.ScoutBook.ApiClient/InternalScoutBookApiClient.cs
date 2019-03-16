using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BSA.ScoutBook.ApiClient.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Refit;

namespace BSA.ScoutBook.ApiClient
{
    internal class InternalScoutBookApiClient : IScoutBookApiClient
    {
        private readonly IInternalScoutBookApiClient _apiClient;
        private string CSRF;
        private readonly Regex _csrfRegex;
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpClientHandler;
        public InternalScoutBookApiClient()
        {
            _csrfRegex = new Regex("<input type=\"hidden\" name=\"CSRF\" value=\"(.*)\" \\/>");
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.Proxy = new WebProxy("http://127.0.0.1:9000");
            _httpClientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://www.scoutbook.com");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.75 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            _httpClient.BaseAddress = new Uri("https://www.scoutbook.com");
            _apiClient = Refit.RestService.For<IInternalScoutBookApiClient>(_httpClient);
        }

        public async Task PreLogin()
        {
            var htmlData = await _apiClient.PreLogin().ConfigureAwait(false);
            CSRF = GetCsrfFromHtmlData(htmlData);
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            loginRequest.CSRF = CSRF;
            var htmlData = await _apiClient.Login(loginRequest.ToDictionary()).ConfigureAwait(false);
            CSRF = GetCsrfFromHtmlData(htmlData);
            return GetLoginResponse(htmlData);
        }

        public async Task<RosterResponse> Roster(RosterRequest rosterRequest)
        {
            _httpClientHandler.CookieContainer.Add(new Cookie("Dashboard_Section", "Reports","/","www.scoutbook.com"));
            _httpClientHandler.CookieContainer.Add(new Cookie("BannerID3_Dismissed", "1", "/", "www.scoutbook.com"));
            _httpClientHandler.CookieContainer.Add(new Cookie("BannerID4_Dismissed", "1", "/", "www.scoutbook.com"));
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://www.scoutbook.com/mobile/dashboard/reports/roster.asp");
            var htmlData = await _apiClient.Roster(rosterRequest.ToDictionary()).ConfigureAwait(false);
            CSRF = GetCsrfFromHtmlData(htmlData);
            return GetRosterResponse(htmlData);
        }

        private string GetCsrfFromHtmlData(string htmlData)
        {
            var match = _csrfRegex.Match(htmlData);
            string ret = match.Groups[1].Value;
            return ret;
        }
        private LoginResponse GetLoginResponse(string htmlData)
        {
            string ret = null;
            return new LoginResponse();
        }
        private RosterResponse GetRosterResponse(string htmlData)
        {
            var scounts = new List<Scout>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlData);
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                var rows = table.SelectNodes("tr");
                rows.Remove(0);
                rows.Remove(1);
                foreach (HtmlNode row in rows)
                {
                    var cells = row.SelectNodes("th|td");
                    var fullname = cells[0].InnerHtml.Trim();
                    var den = cells[1].InnerHtml.Trim();
                    var fullnameSplit = fullname.Split(',');
                    var firstname = fullnameSplit[1].Trim();
                    var lastname = fullnameSplit[0].Trim();

                    var newScount = new Scout(firstname,lastname,den);
                    scounts.Add(newScount);

                }
            }
            return new RosterResponse(scounts);
        }
    }
}
