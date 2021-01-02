using Mh.Business.Bo.Pos;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Pos
{
    public interface IPosBusiness
    {
        ResponseBo<List<PosProductShortCutListBo>> GetShortCutList(long shopId, long operatorRealId, Enums.Languages languageId);

        ResponseBo SaveShortCut(PosProductShortCutBo saveBo);
        ResponseBo DeleteShortCut(PosProductShortCutBo deleteBo);

        ResponseBo SaveShortCutGroup(PosProductShortCutGroupBo saveBo);
        ResponseBo DeleteShortCutGroup(PosProductShortCutGroupBo deleteBo);
    }
}