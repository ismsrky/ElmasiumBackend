namespace Mh.Sessions
{
    public class SessionMyPerson
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public Enums.PersonTypes PersonTypeId { get; set; }

        public Enums.Currencies DefaultCurrencyId { get; set; }
        public Enums.Currencies SelectedCurrencyId { get; set; }

        public Enums.RelationTypes RelationTypeId { get; set; }

        public SessionMyPerson Copy()
        {
            SessionMyPerson person = new SessionMyPerson();

            person.Id = Id;
            person.FullName = FullName;
            person.PersonTypeId = PersonTypeId;
            person.DefaultCurrencyId = DefaultCurrencyId;
            person.SelectedCurrencyId = SelectedCurrencyId;

            person.RelationTypeId = RelationTypeId;

            return person;
        }
    }
}