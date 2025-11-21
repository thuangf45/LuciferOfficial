using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("account_identity")] // Gắn bảng SQL
    public class AccountIdentity : IValidation
    {
        [Key]
        [Column("identity_id")]
        public long IdentityId { get; set; }

        [Column("identity_guid")]
        public Guid IdentityGuid { get; set; }

        [Column("provider")]
        public string Provider { get; set; }

        [Column("provider_key")]
        public string ProviderKey { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; } // mật khẩu hash, chỉ dùng cho provider = 'local'

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("last_used")]
        public DateTime? LastUsed { get; set; }

        [Column("is_verified")]
        public bool IsVerified { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
