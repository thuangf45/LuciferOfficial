using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("shop")] // Gắn bảng SQL
    public class Shop : IValidation
    {
        [Key]
        [Column("shop_id")]
        public long ShopId { get; set; }

        [Column("shop_guid")]
        public Guid ShopGuid { get; set; }

        [Column("shop_name")]
        public string ShopName { get; set; }

        [Column("shop_description")]
        public string? ShopDescription { get; set; }

        [Column("shop_address")]
        public string? ShopAddress { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("avg_rating")]
        public decimal AvgRating { get; set; }

        [Column("shop_coin")]
        public decimal ShopCoin { get; set; }

        [Column("number_item")]
        public int NumberItem { get; set; }

        [Column("number_order")]
        public int NumberOrder { get; set; }

        [Column("number_review")]
        public int NumberReview { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
