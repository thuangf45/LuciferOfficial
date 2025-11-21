using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("item")] // Gắn bảng SQL
    public class Item : IValidation
    {
        [Key]
        [Column("item_id")]
        public long ItemId { get; set; }

        [Column("item_guid")]
        public Guid ItemGuid { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("item_description")]
        public string? ItemDescription { get; set; }

        [Column("media_links")]
        public string? MediaLinks { get; set; } // JSON array link ảnh/video

        [Column("avg_rating")]
        public decimal AvgRating { get; set; }

        [Column("number_review")]
        public int NumberReview { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("shop_id")]
        public long ShopId { get; set; }

        [Column("shop_guid")]
        public Guid ShopGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
