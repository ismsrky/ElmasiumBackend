using Mh.Business.Bo.EnumsOp;
using Mh.Business.Bo.Person.Relation;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mh.Business
{
    public static class Stc
    {
        public static string ConnStr
        {
            get
            {
                return DbAccess.Connection.ConnectionString;
            }
            set
            {
                DbAccess.Connection.ConnectionString = value;
            }
        }

        public static List<DicItem> DicItemList { get; set; }

        public static string GetDicValue(int Id, Enums.Languages language, params string[] param)
        {
            if (Stc.DicItemList == null || Stc.DicItemList.Count == 0)
                return "";

            DicItem item = Stc.DicItemList.Where(x => x.Id == Id).FirstOrDefault();
            if (item == null) return "";

            return GetbyLang(item, language);
        }
        public static string GetDicValue(string key, Enums.Languages language, params string[] param)
        {
            if (Stc.DicItemList == null || Stc.DicItemList.Count == 0)
                return key;

            DicItem item = Stc.DicItemList.Where(x => x.Key == key).FirstOrDefault();
            if (item == null) return key;

            return GetbyLang(item, language);
        }
        //protected string GetDicValue(string key, params string[] param)
        //{
        //    Enums.Languages language = Enums.Languages.Turkce;
        //    if (Session != null)
        //    {
        //        RealPersonBo realPersonBo = Session.RealPerson;
        //        if (realPersonBo != null)
        //            language = realPersonBo.LanguageId;
        //    }

        //    return GetDicValue(key, language, param);
        //}
        public static string GetbyLang(DicItem dicItem, Enums.Languages language)
        {
            switch (language)
            {
                case Enums.Languages.xTurkish:
                    return dicItem.Tr;
                case Enums.Languages.xEnglish:
                    return dicItem.En;
                default:
                    return dicItem.Tr;
            }
        }

        public static Guid GetDicChangeSetId(Enums.Languages langId)
        {
            Guid result;

            DicItem dicItem = DicItemList.Where(x => x.Id == -1).First();

            switch (langId)
            {
                case Enums.Languages.xTurkish:
                    result = dicItem.Tr.ToGuid();
                    break;
                case Enums.Languages.xEnglish:
                    result = dicItem.En.ToGuid();
                    break;
                default:
                    result = dicItem.Tr.ToGuid();
                    break;
            }

            return result;
        }
        public static bool NeedToSendDics(string changeSetId,Enums.Languages langId)
        {
            bool result = false;

            if (changeSetId.IsGuid())
            {
                Guid dicChangeSetId = changeSetId.ToGuid();
                Guid dicChangeSetIdMe = Business.Stc.GetDicChangeSetId(langId);
                if (dicChangeSetId != dicChangeSetIdMe)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public static string DictionaryProcessText(string text, Enums.Languages languageId)
        {
            string result = text;

            Regex regex = new Regex("{{x(.+?)}}");

            MatchCollection mc = regex.Matches(text);
            if (mc != null && mc.Count > 0)
            {
                string word = null;
                foreach (Match item in mc)
                {
                    word = "x" + item.Groups[1].ToString();
                    result = result.Replace("{{" + word + "}}", Business.Stc.GetDicValue(word, languageId));
                }
            }

            return result;
        }

        // Enums
        public static List<ShopTypeBo> EnumShopTypeList { get; set; }
        public static List<CurrenciesBo> EnumCurrencyList { get; set; }
        public static List<FicheContentBo> EnumFicheContentList { get; set; }

        public static List<PersonRelationRuleListBo> PersonRelationRuleList { get; set; }
    }
}