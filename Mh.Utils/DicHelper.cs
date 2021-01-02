using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Utils
{
    public class DicHelper
    {
        public enum Languages
        {
            Turkce = 0,
            English = 1
        }

        public List<DicItem> DicItemList { get; private set; }
        public Languages Language { get; private set; }
        public DicHelper(ref List<DicItem> itemList, Languages language)
        {
            DicItemList = itemList;
            Language = language;
        }

        public string Get(string xKey, params string[] param)
        {
            if (DicItemList == null || DicItemList.Count == 0)
                return xKey;

            DicItem item = DicItemList.Where(x => x.Key == xKey).FirstOrDefault();
            if (item == null) return xKey;

            return GetbyLang(item);
        }
        public string Get(int Id)
        {
            if (DicItemList == null || DicItemList.Count == 0)
                return "";

            DicItem item = DicItemList.Where(x => x.Id == Id).FirstOrDefault();
            if (item == null) return "";

            return GetbyLang(item);
        }

        string GetbyLang(DicItem dicItem)
        {
            switch (Language)
            {
                case Languages.Turkce:
                    return dicItem.Tr;
                case Languages.English:
                    return dicItem.En;
                default:
                    return dicItem.Tr;
            }
        }


    }

    public class DicItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public bool IsForDesign { get; set; }
        public string Tr { get; set; }
        public string En { get; set; }
    }
}