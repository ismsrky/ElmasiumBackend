namespace Mh.Service.Dto.Person.Real
{
    public class RealPersonDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Enums.Languages LanguageId { get; set; }

        public Enums.Genders? GenderId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }

        public string Phone { get; set; }
        public double? BirthdateNumber { get; set; }
    }
}