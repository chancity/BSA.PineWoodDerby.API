using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BSA.ScoutBook.ApiClient.Models;
using Refit;

namespace BSA.ScoutBook.ApiClient
{
    internal interface IInternalScoutBookApiClient
    {
        [Get("/mobile/login.asp ")]
        Task<string> PreLogin();

        [Post("/mobile/login.asp?Source=&Redir=")]
        Task<string> Login([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        [Post("/mobile/dashboard/reports/roster.asp?Action=Print&DenID=&PatrolID=&UnitID=")]
        Task<string> Roster([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

    }
}
