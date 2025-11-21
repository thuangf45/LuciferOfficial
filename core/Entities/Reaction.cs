using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("reaction")] // Gắn bảng SQL
    public class Reaction : IValidation
    {
        [Key]
        [Column("reaction_id")]
        public long ReactionId { get; set; }

        [Column("reaction_guid")]
        public Guid ReactionGuid { get; set; }

        [Column("reaction_type")]
        public string? ReactionType { get; set; } // like, heart, haha, huhu

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("post_id")]
        public long PostId { get; set; }

        [Column("post_guid")]
        public Guid PostGuid { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
