using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("voucher")] // Gắn bảng SQL
    public class Voucher : IValidation
    {
        [Key]
        [Column("voucher_id")]
        public long VoucherId { get; set; }

        [Column("voucher_guid")]
        public Guid VoucherGuid { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("discount_type")]
        public string DiscountType { get; set; } // "percent" hoặc "amount"

        [Column("discount_value")]
        public decimal DiscountValue { get; set; }

        [Column("max_discount")]
        public decimal? MaxDiscount { get; set; }

        [Column("valid_from")]
        public DateTime ValidFrom { get; set; }

        [Column("valid_to")]
        public DateTime ValidTo { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("max_usage")]
        public int MaxUsage { get; set; }

        [Column("used_count")]
        public int UsedCount { get; set; }

        [Column("used_users")]
        public string? UsedUsers { get; set; } // JSON array GUID người dùng đã dùng

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual bool Validate() => true;
    }
}
