using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.App.Models
{
    public class CollectionModel
    {
        public Collection Collection { get; set; }

        public string RawList { get; set; }
    }
}