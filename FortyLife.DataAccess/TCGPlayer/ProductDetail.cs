using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FortyLife.DataAccess.TCGPlayer
{
    public class ProductDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string CleanName { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int GroupId { get; set; }

        public string Url { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CacheDate { get; set; }
    }
}
