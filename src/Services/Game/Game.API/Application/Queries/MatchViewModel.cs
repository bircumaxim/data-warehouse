using System;

namespace Game.API.Application.Queries
{
    public class Match
    {
        public DateTime MatchDate { get; set; }
        public string MatchStatus { get; set; }

        private string _matchInitiatorId;
        private string _secondPartenerId;

        private string _description;
    }

    public class OrderSummary
    {
        public int ordernumber { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public double total { get; set; }
    }

    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}