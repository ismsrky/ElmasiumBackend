namespace Mh.Service.Dto.Product.Code
{
    public class ProductCodeListDto
    {
        public long Id { get; set; }

        public string Code { get; set; }
        public Enums.ProductCodeTypes ProductCodeTypeId { get; set; }

        public double CreateDateNumber { get; set; }
        public double? UpdateDateNumber { get; set; }
    }
}