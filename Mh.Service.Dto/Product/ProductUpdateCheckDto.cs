namespace Mh.Service.Dto.Product
{
    public class ProductUpdateCheckDto
    {
        public long ProductId { get; set; }

        public Enums.ProductUpdateTypes ProductUpdateTypeId { get; set; }
    }
}