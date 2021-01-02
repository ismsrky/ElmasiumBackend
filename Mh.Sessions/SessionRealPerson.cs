using System.Collections.Generic;

namespace Mh.Sessions
{
    public class SessionRealPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Enums.Languages LanguageId { get; set; }
        public Enums.Currencies DefaultCurrencyId { get; set; }
        public Enums.Currencies SelectedCurrencyId { get; set; }
        public Enums.Genders GenderId { get; set; }

        public long PersonRelationId { get; set; }

        public long ApiSessionId { get; set; }

        public List<long> MyPersonIdList { get; set; }

        public SessionRealPerson Copy()
        {
            SessionRealPerson person = new SessionRealPerson();

            person.Id = Id;
            person.Name = Name;
            person.Surname = Surname;
            person.LanguageId = LanguageId;
            person.GenderId = GenderId;

            person.PersonRelationId = PersonRelationId;

            person.DefaultCurrencyId = DefaultCurrencyId;
            person.SelectedCurrencyId = SelectedCurrencyId;

            return person;
        }
    }
}