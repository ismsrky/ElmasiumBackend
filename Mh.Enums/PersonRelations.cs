using System.ComponentModel;

namespace Mh.Enums
{
    public enum PersonRelations
    {
        [Description("Şube")]
        Branch = 0,
        [Description("Aile Üyesi")]
        FamilyMember = 1,
        [Description("Çalışan")]
        Stuff = 2
    }
}