using System;
using System.Linq;
using RangedDPS.StatUtilities;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_TurretWeaponDPS : StatWorker_TurretDPSBase
{
    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
    {
        if (!ShouldShowFor(req))
        {
            return 0f;
        }

        RangedWeaponStats weaponStats = GetTurretStats(req);

        var bestAccuracy = new[]
        {
            weaponStats.AccuracyTouch,
            weaponStats.AccuracyShort,
            weaponStats.AccuracyMedium,
            weaponStats.AccuracyLong
        }.Max();

        return weaponStats.GetRawDps().Average * Math.Min(bestAccuracy, 1f);
    }

    public override string GetStatDrawEntryLabel(StatDef stat, float value, ToStringNumberSense numberSense,
        StatRequest optionalReq, bool finalized = true)
    {
        RangedWeaponStats weaponStats = GetTurretStats(optionalReq);

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
        if (!ShouldShowFor(req))
        {
            return "";
        }

        RangedWeaponStats weaponStats = GetTurretStats(req);
        return DpsRangeBreakdown(weaponStats);
    }
}