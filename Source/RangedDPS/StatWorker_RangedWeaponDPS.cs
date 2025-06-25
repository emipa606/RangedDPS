using System;
using System.Linq;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_RangedWeaponDPS : StatWorker_RangedDPSBase
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
        if (!ShouldShowFor(req))
        {
            return 0f;
        }

        var weaponStats = GetWeaponStats(req);

        var bestAccuracy = new[]
        {
            weaponStats.AccuracyTouch,
            weaponStats.AccuracyShort,
            weaponStats.AccuracyMedium,
            weaponStats.AccuracyLong
        }.Max();

        return weaponStats.GetRawDps().Average * Math.Min(bestAccuracy, 1);
    }

    public override string GetStatDrawEntryLabel(StatDef stat, float value, ToStringNumberSense numberSense,
        StatRequest optionalReq, bool finalized = true)
    {
        var weaponStats = GetWeaponStats(optionalReq);

        var optimalRange = new[]
        {
            (weaponStats.AccuracyTouch, (int)ShootTuning.DistTouch),
            (weaponStats.AccuracyShort, (int)ShootTuning.DistShort),
            (weaponStats.AccuracyMedium, (int)ShootTuning.DistMedium),
            (weaponStats.AccuracyLong, (int)ShootTuning.DistLong)
        }.MaxBy(acc => acc.Item1);

        return
            $"{value.ToStringByStyle(stat.toStringStyle, numberSense)} ({string.Format("StatsReport_RangedDPSOptimalRange".Translate(), optimalRange.Item2)})";
    }

    public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
    {
        return !ShouldShowFor(req) ? "" : DpsRangeBreakdown(GetWeaponStats(req));
    }
}