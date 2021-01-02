using System.ComponentModel;

namespace Mh.Enums
{
    public enum RelationTypes
    {
        // 'DefaultValue' means 'IsMaster'. I did not want to make independent enum indeed.

        [DefaultValue(true)]
        xMyself = 0,

        [DefaultValue(false)]
        xFriend = 1,

        [DefaultValue(true)]
        xStaff = 2,

        [DefaultValue(true)]
        xShopOwner = 3,

        [DefaultValue(false)]
        xMyShop = 4,

        [DefaultValue(false)]
        xBranch = 5,

        [DefaultValue(false)]
        xCustomer = 6,

        [DefaultValue(false)]
        xSeller = 7,

        [DefaultValue(true)]
        xNeighbor = 8,

        [DefaultValue(false)]
        xShopIWorkedFor = 9
    }
}