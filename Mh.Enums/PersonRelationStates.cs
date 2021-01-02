using System.ComponentModel;

namespace Mh.Enums
{
    public enum PersonRelationStates
    {
        [Description("İstek Gönderildi/Bekliyor")]
        Pending = 0,
        [Description("Kabul edildi")]
        Accepted = 1,
        [Description("Reddedildi")]
        Declined = 2
    }
}