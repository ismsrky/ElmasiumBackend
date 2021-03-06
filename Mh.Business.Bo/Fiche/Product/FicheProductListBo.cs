﻿namespace Mh.Business.Bo.Fiche.Product
{
    public class FicheProductListBo
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductTypes ProductTypeId { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public string Notes { get; set; }

        public bool IsDeleted { get; set; }
    }
}