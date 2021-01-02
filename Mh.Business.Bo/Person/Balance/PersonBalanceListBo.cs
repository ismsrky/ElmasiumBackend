namespace Mh.Business.Bo.Person.Balance
{
    public class PersonBalanceListBo
    {
        /// <summary>
        /// This class is used to present balance between two people.
        /// </summary>
        public Enums.Currencies CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public bool IsDebt { get; set; }
    }
}