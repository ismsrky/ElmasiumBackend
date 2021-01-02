namespace Mh.Service.Dto.Fiche.Relation
{
    public class FicheRelationSaveDto
    {
        public long ChildFicheId { get; set; }
        public Enums.FicheRelationTypes FicheRelationTypeId { get; set; }
    }
}