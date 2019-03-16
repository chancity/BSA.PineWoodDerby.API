using System;
using BSA.ScoutBook.ApiClient;
using BSA.ScoutBook.ApiClient.Models;
using Newtonsoft.Json;

namespace BSA.ScoutBook.ApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var scoutBookApiClient = new ScoutBookApiClient();
            var loginRequest = new LoginRequest(args[0], args[1]);
            var loginResult = scoutBookApiClient.Login(loginRequest).Result;

            var rosterRequest = new RosterRequest(int.Parse(args[2]), true,true);
            var rosterResult = scoutBookApiClient.Roster(rosterRequest).Result;
            
            Console.WriteLine(JsonConvert.SerializeObject(rosterResult, Formatting.Indented));
            Console.ReadLine();
        }
    }
}
