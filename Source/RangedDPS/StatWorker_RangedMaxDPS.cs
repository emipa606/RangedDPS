using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_RangedMaxDPS : StatWorker_RangedDPSBase
{
    public override bool ShouldShowFor(StatRequest req)
    {
        if (base.ShouldShowFor(req))
        {
            return req.Def is ThingDef { IsRangedWeapon: true };
        }

        return false;
    }

    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
    {
        return !ShouldShowFor(req) ? 0f : GetWeaponStats(req).GetRawDps().Average;
    }
}