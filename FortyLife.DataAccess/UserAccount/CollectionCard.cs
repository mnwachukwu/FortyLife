using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FortyLife.DataAccess.UserAccount
{
    public class CollectionCard
    {
        [Key, Column(Order = 1)]
        public string Name { get; set; }

        [Key, Column(Order = 2)]
        public string SetCode { get; set; }

        [Key, Column(Order = 3)]
        public bool Foil { get; set; }

        public bool Commander { get; set; }

        public int Count { get; set; }

        [Key, Column(Order = 0)]
        public int CollectionId { get; set; }
    }
}
