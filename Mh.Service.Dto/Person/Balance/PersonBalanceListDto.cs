namespace Mh.Service.Dto.Person.Balance
{
    public class PersonBalanceListDto
    {
        /// <summary>
        /// This class is used to present balance between two people.
        /// </summary>
        public Enums.Currencies CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public bool IsDebt { get; set; }
    }
}