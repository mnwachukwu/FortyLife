﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FortyLife.DataAccess.UserAccount
{
    public class CollectionCard
    {
        public int CollectionCardId { get; set; }

        public string Name { get; set; }

        public string SetCode { get; set; }

        public bool Foil { get; set; }

        public bool Commander { get; set; }

        public int Count { get; set; }

        public int? CollectionId { get; set; }
    }
}
