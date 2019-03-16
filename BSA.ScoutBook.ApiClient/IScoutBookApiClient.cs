using System.Threading.Tasks;
using BSA.ScoutBook.ApiClient.Models;


namespace BSA.ScoutBook.ApiClient
{
    public interface IScoutBookApiClient
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<RosterResponse> Roster(RosterRequest rosterRequest);
    }
}
