using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("film")] // Gắn bảng SQL
    public class Film : IValidation
    {
        [Key]
        [Column("film_id")]
        public long FilmId { get; set; }

        [Column("film_guid")]
        public Guid FilmGuid { get; set; }

        [Column("film_name")]
        public string FilmName { get; set; }

        [Column("film_description")]
        public string FilmDescription { get; set; }

        [Column("files")]
        public string? Files { get; set; } // JSON array URL/tập phim

        [Column("film_cost")]
        public decimal FilmCost { get; set; }

        [Column("avg_rating")]
        public decimal AvgRating { get; set; }

        [Column("number_review")]
        public int NumberReview { get; set; }

        [Column("number_view")]
        public int NumberView { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        public virtual bool Validate() => true;
    }
}
