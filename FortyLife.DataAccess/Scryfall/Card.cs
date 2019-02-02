using System;
using System.Collections.Generic;

namespace FortyLife.DataAccess.Scryfall
{
    public class Card : CardFace
    {
        public string Id { get; set; }

        public string OracleId { get; set; }

        public List<int> MultiverseIds { get; set; }

        public int MtgoId { get; set; }

        public int MtgoFoilId { get; set; }

        public int TcgPlayerId { get; set; }

        public string Lang { get; set; }

        public DateTime ReleasedAt { get; set; }

        public string Uri { get; set; }

        public string ScryfallUri { get; set; }

        public string Layout { get; set; }

        public bool HiResImage { get; set; }

        public double Cmc { get; set; }

        public List<string> ColorIdentity { get; set; }

        public Legalities Legalities { get; set; }

        public List<string> Games { get; set; }

        public bool Reserved { get; set; }

        public bool Foil { get; set; }

        public bool NonFoil { get; set; }

        public bool Oversized { get; set; }

        public bool Promo { get; set; }

        public bool Reprint { get; set; }

        public string Set { get; set; }

        public string SetName { get; set; }

        public string SetUri { get; set; }

        public string SetSearchUri { get; set; }

        public string ScryfallSetUri { get; set; }

        public string RulingsUri { get; set; }

        public string PrintsSearchUri { get; set; }

        public string CollectorNumber { get; set; }

        public bool Digital { get; set; }

        public string Rarity { get; set; }

        public string Watermark { get; set; }

        public string BorderColor { get; set; }

        public string Frame { get; set; }

        public string FrameEffect { get; set; }

        public bool FullArt { get; set; }

        public bool StorySpotlight { get; set; }

        public int EdhrecRank { get; set; }

        public string Usd { get; set; }

        public string Eur { get; set; }

        public string Tix { get; set; }

        public RelatedUris RelatedUris { get; set; }

        public PurchaseUris PurchaseUris { get; set; }

        public List<CardFace> CardFaces { get; set; }

        // Non-Scryfall data
        public bool IsDoubleFaced => CardFaces != null;
    }

    public class ImageUris
    {
        public string Small { get; set; }

        public string Normal { get; set; }

        public string Large { get; set; }

        public string Png { get; set; }

        public string ArtCrop { get; set; }

        public string BorderCrop { get; set; }
    }

    public class Legalities
    {
        public string Standard { get; set; }

        public string Future { get; set; }

        public string Frontier { get; set; }

        public string Modern { get; set; }

        public string Legacy { get; set; }

        public string Pauper { get; set; }

        public string Vintage { get; set; }

        public string Penny { get; set; }

        public string Commander { get; set; }

        public string Duel { get; set; }

        public string OldSchool { get; set; }
    }

    public class RelatedUris
    {
        public string Gatherer { get; set; }

        public string TcgPlayerDecks { get; set; }

        public string EdhRec { get; set; }

        public string MtgTop8 { get; set; }
    }

    public class PurchaseUris
    {
        public string TcpPlayer { get; set; }

        public string CardMarket { get; set; }

        public string CardHoarder { get; set; }
    }

    public class CardFace
    {
        public string Object { get; set; }

        public string Name { get; set; }

        public string ManaCost { get; set; }

        public string TypeLine { get; set; }

        public string OracleText { get; set; }

        public List<string> Colors { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string FlavorText { get; set; }

        public string Artist { get; set; }

        public string IllustrationId { get; set; }

        public ImageUris ImageUris { get; set; }
        
        public string Loyalty { get; set; }
    }
}
