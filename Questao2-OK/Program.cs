using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;

        using (HttpClient client = new HttpClient())
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}";
            var response = client.GetStringAsync(url).Result;
            var data = JsonConvert.DeserializeObject<FootballMatchResponse>(response);

            if (data != null && data.data != null)
            {
                foreach (var match in data.data)
                {
                    if (!string.IsNullOrEmpty(match.team1goals))
                    {
                        totalGoals += int.Parse(match.team1goals);
                    }
                }
            }

            url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}";
            response = client.GetStringAsync(url).Result;
            data = JsonConvert.DeserializeObject<FootballMatchResponse>(response);

            if (data != null && data.data != null)
            {
                foreach (var match in data.data)
                {
                    if (!string.IsNullOrEmpty(match.team2goals))
                    {
                        totalGoals += int.Parse(match.team2goals);
                    }
                }
            }
        }

        return totalGoals;
    }
}

    public class FootballMatchResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public Match[] data { get; set; }
    }

    public class Match
    {
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }