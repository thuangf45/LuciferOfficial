using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("cart")] // Gắn bảng SQL
    public class Cart : IValidation
    {
        [Key]
        [Column("cart_id")]
        public long CartId { get; set; }

        [Column("cart_guid")]
        public Guid CartGuid { get; set; }

        [Column("cart_details")]
        public string? CartDetails { get; set; } // JSON mảng item: item_id, quantity, price

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
