namespace Mh.Service.Dto.Product.Code
{
    public class ProductCodeDto
    {
        public string Code { get; set; }
        public Enums.ProductCodeTypes ProductCodeTypeId { get; set; }

        public long ProductId { get; set; }
    }
}