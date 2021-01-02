using System.ComponentModel;

namespace Mh.Enums
{
    public enum PersonTypeGroups
    {
        [Description("Gerçek")]
        Real = 0,
        [Description("Dükkan")]
        Shop = 1,
        [Description("Aile")]
        Family = 2,
        [Description("Etkinlik")]
        Activity = 3,
        [Description("Topluluk")]
        Society = 4,
        [Description("Site/Aparman ev")]
        House = 5,
    }
}