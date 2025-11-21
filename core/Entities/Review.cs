using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("review")] // Gắn bảng SQL
    public class Review : IValidation
    {
        [Key]
        [Column("review_id")]
        public long ReviewId { get; set; }

        [Column("review_guid")]
        public Guid ReviewGuid { get; set; }

        [Column("target_type")]
        public string TargetType { get; set; } // "item" hoặc "shop"

        [Column("rating")]
        public int Rating { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        [Column("target_id")]
        public long TargetId { get; set; }

        [Column("target_guid")]
        public Guid TargetGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
