namespace Mh.Service.Dto.Basket
{
    public class BasketGetListCriteriaDto
    {
        public long DebtPersonId { get; set; }
        public Enums.Currencies CurrencyId { get; set; }

        public long? BasketId { get; set; }
    }
}