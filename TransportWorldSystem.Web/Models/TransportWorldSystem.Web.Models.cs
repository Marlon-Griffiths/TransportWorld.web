namespace TransportWorldSystem.Web.Models
{
    public class AboutUsViewModel
    {
        public string CompanyHistory { get; set; }
        public string MissionStatement { get; set; }
        public List<string> CoreValues { get; set; }
        public List<Leader> LeadershipTeam { get; set; }
    }

    public class Leader
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }
    }
}
