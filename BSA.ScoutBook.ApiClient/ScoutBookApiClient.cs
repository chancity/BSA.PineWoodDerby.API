using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BSA.ScoutBook.ApiClient.Models;

namespace BSA.ScoutBook.ApiClient
{
    public class ScoutBookApiClient : IScoutBookApiClient
    {
        private readonly InternalScoutBookApiClient _apiClient;

        public ScoutBookApiClient()
        {
            _apiClient = new InternalScoutBookApiClient();
        }


        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            await _apiClient.PreLogin().ConfigureAwait(false);
            return await _apiClient.Login(loginRequest).ConfigureAwait(false);
        }

        public async Task<RosterResponse> Roster(RosterRequest rosterRequest)
        {
            return await _apiClient.Roster(rosterRequest).ConfigureAwait(false);
        }
    }
}
