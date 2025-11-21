using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("account")] // Gắn bảng SQL
    public class Account : IValidation
    {
        [Key]
        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("user_address")]
        public string? UserAddress { get; set; }

        [Column("birthday")]
        public DateTime? Birthday { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("reputation_score")]
        public int ReputationScore { get; set; }

        [Column("number_follower")]
        public int NumberFollower { get; set; }

        [Column("number_following")]
        public int NumberFollowing { get; set; }

        [Column("number_post")]
        public int NumberPost { get; set; }

        [Column("account_number")]
        public string? AccountNumber { get; set; }

        [Column("account_amount")]
        public long AccountAmount { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        public virtual bool Validate() => true;
    }
}
