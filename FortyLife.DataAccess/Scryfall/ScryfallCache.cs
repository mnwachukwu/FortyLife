using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace FortyLife.DataAccess.Scryfall
{
    public static class ScryfallCache
    {
        private static MemoryCache cache;
        private static ScryfallRequestEngine scryfallRequestEngine;

        public static void Init()
        {
            cache = new MemoryCache("main_cache");
            scryfallRequestEngine = new ScryfallRequestEngine();
        }

        public static void Clear()
        {
            foreach (var key in GetAllCacheKeys())
            {
                cache.Remove(key);
            }
        }

        private static IEnumerable<string> GetAllCacheKeys()
        {
            return cache.Select(item => item.Key);
        }

        public static Card GetCachedCard(string cardName)
        {
            var key = $"{cardName}_card";

            if (!cache.Contains(key))
            {
                var card = scryfallRequestEngine.GetCard(cardName);
                // TODO: Make sure the card being added is legit
                if (card != null)
                    cache.Set($"{cardName}_card", card, DateTime.Now.AddDays(7));
            }

            return (Card)cache[key];
        }
    }
}
