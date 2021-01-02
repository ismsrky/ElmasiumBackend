using System.ComponentModel;

namespace Mh.Enums
{
    public enum ProductSnycTypes
    {
        [Description("Yok")]
        None = 0,
        [Description("Tam")]
        Full = 1,
        [Description("Sadece Satış Fiyatı")]
        OnlySalePrice = 2,
        [Description("Sadece Alış Fiyatı")]
        OnlyPurhasePrice = 3
    }
}