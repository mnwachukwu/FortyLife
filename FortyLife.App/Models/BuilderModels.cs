using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FortyLife.DataAccess.Scryfall;
using FortyLife.DataAccess.TCGPlayer;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.App.Models
{
    public class EditCollectionModel
    {
        public Collection Collection { get; set; }

        public string RawList { get; set; }
    }

    public class ViewCollectionModel
    {
        public List<Card> ScryfallList { get; set; }

        public Collection Collection { get; set; }

        public List<double> Prices { get; set; }

        public string OwnerDisplayName { get; set; }

        public int OwnerId { get; set; }
    }
}