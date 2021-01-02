namespace Mh.Business.Bo.Person.Relation.Find
{
    public class PersonRelationFindListBo
    {
        public long PersonId { get; set; }
        public Enums.PersonTypes PersonTypeId { get; set; }
        public string FullName { get; set; }

        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        public string Email { get; set; }

        public long? PersonRelationId { get; set; }
        public Enums.ApprovalStats? ApprovalStatId { get; set; }
        public Enums.RelationTypes? ChildRelationTypeId { get; set; }
        public bool IsParent { get; set; }
    }
}