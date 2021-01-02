namespace Mh.Service.Dto.Person.Balance
{
    public class PersonBalanceTotalListDto
    {
        /// <summary>
        /// This class is used to present whole debt and credit value of given person to everyone.
        /// </summary>
        public Enums.Currencies CurrencyId { get; set; }
        public decimal DebtTotal { get; set; }
        public decimal CreditTotal { get; set; }
    }
}