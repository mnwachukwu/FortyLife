using System;
using System.Collections.Generic;

namespace FortyLife.DataAccess.UserAccount
{
    public class Collection
    {
        public int CardCount => Cards?.Count ?? 0;

        public int CollectionId { get; set; }

        public string Name { get; set; }

        public List<CollectionCard> Cards { get; set; }

        public double TcgMidValue { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastEditDate { get; set; }

        // TODO: when implemented, make very sure it cannot be abused! (one view, per IP, per day)
        public int Views { get; set; }
    }
}
