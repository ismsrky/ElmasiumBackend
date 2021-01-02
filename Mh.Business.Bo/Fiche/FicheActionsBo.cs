namespace Mh.Business.Bo.Fiche
{
    public class FicheActionsBo
    {
        public bool Deletable { get; set; }
        public bool Acceptable { get; set; }
        public bool Rejectable { get; set; }
        public bool PullBackable { get; set; }

        public bool Commentable { get; set; }
    }
}