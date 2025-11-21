using LuciferCore.Manager.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.core.Entities
{
    [Table("order")] // Gắn bảng SQL
    public class Order : IValidation
    {
        [Key]
        [Column("order_id")]
        public long OrderId { get; set; }

        [Column("order_guid")]
        public Guid OrderGuid { get; set; }

        [Column("order_name")]
        public string? OrderName { get; set; }

        [Column("order_description")]
        public string? OrderDescription { get; set; }

        [Column("order_details")]
        public string? OrderDetails { get; set; } // JSON mảng item: item_id, quantity, price

        [Column("shop_address")]
        public string ShopAddress { get; set; }

        [Column("shoppers_address")]
        public string? ShoppersAddress { get; set; }

        [Column("shoppers_phone_number")]
        public string? ShoppersPhoneNumber { get; set; }

        [Column("form_shopping")]
        public string FormShopping { get; set; }

        [Column("payment_status")]
        public string PaymentStatus { get; set; }

        [Column("shipping_status")]
        public string? ShippingStatus { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [Column("final_amount")]
        public decimal FinalAmount { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("shop_id")]
        public long ShopId { get; set; }

        [Column("shop_guid")]
        public Guid ShopGuid { get; set; }

        [Column("account_id")]
        public long AccountId { get; set; }

        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        public virtual bool Validate() => true;
    }
}
