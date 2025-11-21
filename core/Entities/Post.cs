using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("post")] // Gắn bảng SQL
    public class Post : IValidation
    {
        [Key]
        [Column("post_id")]
        public long PostId { get; set; }

        [Column("post_guid")]
        public Guid PostGuid { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("media_links")]
        public string? MediaLinks { get; set; } // JSON array link ảnh/video

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("number_comment")]
        public int NumberComment { get; set; }

        [Column("number_reaction")]
        public int NumberReaction { get; set; }

        [Column("parent_id")]
        public long? ParentId { get; set; }

        [Column("parent_guid")]
        public Guid? ParentGuid { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
