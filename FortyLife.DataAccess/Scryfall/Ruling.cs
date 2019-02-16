﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortyLife.DataAccess.Scryfall
{
    public class Ruling
    {
        public int RulingId { get; set; }

        public string RulingUri { get; set; }

        public string Object { get; set; }

        public string OracleId { get; set; }

        public string Source { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string Comment { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
