namespace Mh.Service.Dto.Person.Table
{
    public class PersonTableListDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public Enums.PersonTableStats PersonTableStatId { get; set; }
        public int Order { get; set; }

        public long? LastTableFicheId { get; set; }
        public Enums.TableFicheStats? TableFicheStatId { get; set; }
        public Enums.Currencies? FicheCurrencyId { get; set; }
        public long? FicheDebtPersonId { get; set; }
        public string FicheDebtPersonFullName { get; set; }
        public decimal FicheGrandTotal { get; set; }
        public double? FicheCreateDateNumber { get; set; }

        //public string Duration { get; set; }
    }
}