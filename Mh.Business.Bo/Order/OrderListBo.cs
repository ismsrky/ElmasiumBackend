using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Option;
using Mh.Business.Bo.Product.Code;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Order
{
    public class OrderListBo
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

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string Phone { get; set; }

        public long ShopId { get; set; }
        public string ShopFullName { get; set; } // not null
        public string ShopUrlName { get; set; } // not null
        public long ShopStarCount { get; set; }
        public long ShopStarSum { get; set; }


        // Order product
        public long OrderProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal RowGrandTotal { get; set; }
        public string RowNotes { get; set; }

        // Product
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        // Person product
        public int CategoryId { get; set; }
        public long StarCount { get; set; }
        public long StarSum { get; set; }

        // Product code
        public string CodeListRawJson { get; set; }
        public List<ProductCodeBo> CodeList { get; set; } // not null. At least one row that stock code.


        // Product image
        public Guid? PortraitImageUniqueId { get; set; }
        public Enums.FileTypes? PortraitImageFileTypeId { get; set; }

        // Option
        public string OptionListRawJson { get; set; }
        public List<OptionListBo> OptionList { get; set; } // null

        // Include and Exclude
        public string IncludeExcludeListRawJson { get; set; } // null
        public List<IncludeExcludeBo> IncludeExcludeList { get; set; } // null

        // Comment
        public long? PersonProductCommentId { get; set; }
        public long? PersonCommentId { get; set; }
        public bool IsCommentable { get; set; }
    }
}