using Mh.Utils;

namespace Mh.Business.Bo.Dictionary
{
    public class DictionaryBo
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public bool IsForDesign { get; set; }
        public string Tr { get; set; }
        public string En { get; set; }

        public DicItem ToDicItem()
        {
            DicItem dicItem = new DicItem();
            dicItem.Id = Id;
            dicItem.Key = Key;
            dicItem.IsForDesign = IsForDesign;
            dicItem.Tr = Tr;
            dicItem.En = En;

            return dicItem;
        }
    }
}