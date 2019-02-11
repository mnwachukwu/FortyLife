using System.Collections.Generic;

namespace FortyLife.Core.UserAccount
{
    public enum DeckFormat
    {
        Commander,
        DuelCommander,
        Standard,
        Modern,
        Legacy,
        Vintage,
        Frontier,
        Pauper,
        Penny,
        OldSchool,
        Future
    }

    public class Deck : Collection
    {
        public ColorStatistics ColorStatistics { get; set; }

        public CardTypeStatistics CardTypeStatistics { get; set; }

        public double AverageCmc { get; set; }

        public Dictionary<int, int> CmcCount { get; set; }

        public DeckFormat Format { get; set; }

        public bool Legality { get; set; }

        public bool AllowSuggestions { get; set; }

        public List<Suggestion> Suggestions { get; set; }
    }

    public class ColorStatistics
    {
        public double WhitePct { get; set; }

        public double BluePct { get; set; }

        public double BlackPct { get; set; }

        public double RedPct { get; set; }

        public double GreenPct { get; set; }
    }

    public class CardTypeStatistics
    {
        // TODO: Make these two statistics work right
        //public double NonCreatureNonLandPct => 100 - (CreaturePct + LandPct);
        //public double CreatureNonLandPct => 100 - (CreaturePct + LandPct);

        public double CreaturePct { get; set; }

        public double ArtifactPct { get; set; }

        public double EnchantmentPct { get; set; }

        public double InstantPct { get; set; }

        public double SorceryPct { get; set; }

        public double PlaneswalkerPct { get; set; }

        public double LandPct { get; set; }
    }
}
