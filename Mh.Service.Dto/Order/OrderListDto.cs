using System.Collections.Generic;

namespace Mh.Service.Dto.Order
{
    public class OrderListDto
    {
        public long Id { get; set; }

        public long DebtPersonId { get; set; }
        public string DebtPersonFullName { get; set; }
        public Enums.PersonTypes DebtPersonTypeId { get; set; }

        public long CreditPersonId { get; set; }
        public string CreditPersonFullName { get; set; }
        public Enums.PersonTypes CreditPersonTypeId { get; set; }

        public Enums.OrderStats OrderStatId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }
        public decimal GrandTotal { get; set; }

        public string Notes { get; set; }

        public bool IsReturn { get; set; }
        public long? RelatedOrderId { get; set; }

        public long BasketId { get; set; }
        public long DeliveryAddressId { get; set; }

        public double CreateDateNumber { get; set; }
        public double? UpdateDateNumber { get; set; }

        public string Phone { get; set; }

        public long ShopId { get; set; }
        public string ShopFullName { get; set; } // not null
        public string ShopUrlName { get; set; } // not null
        public long ShopStarCount { get; set; }
        public long ShopStarSum { get; set; }

        public long? CommentId { get; set; }
        public bool IsCommentable { get; set; }

        public List<Product.OrderProductListDto> ProductList { get; set; }
    }
}