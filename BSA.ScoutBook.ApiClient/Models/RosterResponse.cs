using System.Collections.Generic;

namespace BSA.ScoutBook.ApiClient.Models
{
    public class RosterResponse
    {
        public List<Scout> Scouts { get; }
        public RosterResponse(List<Scout> scouts)
        {
            Scouts = scouts;
        }
    }

    public class Scout
    {
        public string Firstname { get; }
        public string Lastname { get; }
        public string Den { get; }
        public Scout(string firstname, string lastname, string den)
        {
            Firstname = firstname;
            Lastname = lastname;
            Den = den;
        }
    }
}
