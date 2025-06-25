using RangedDPS.StatUtilities;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_TurretShooterDPS : StatWorker_TurretDPSBase
{
    public override bool IsDisabledFor(Thing thing)
    {
        return base.IsDisabledFor(thing) || StatDefOf.ShootingAccuracyTurret.Worker.IsDisabledFor(thing);
    }

    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
    {
        if (!ShouldShowFor(req))
        {
            return 0f;
        }

        var turret = GetTurret(req);
        RangedWeaponStats weaponStats = GetTurretStats(req);

        var optimalRange = weaponStats.FindOptimalRange(turret);
        return weaponStats.GetAdjustedDps(optimalRange, turret).Average;
    }

    public override string GetStatDrawEntryLabel(StatDef stat, float value, ToStringNumberSense numberSense,
        StatRequest optionalReq, bool finalized = true)
    {
        var turret = GetTurret(optionalReq);
        RangedWeaponStats weaponStats = GetTurretStats(optionalReq);

        var optimalRange = (int)weaponStats.FindOptimalRange(turret);

        return
            $"{value.ToStringByStyle(stat.toStringStyle, numberSense)} ({string.Format("StatsReport_RangedDPSOptimalRange".Translate(), optimalRange)})";
    }

    public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
    {
        if (!ShouldShowFor(req))
        {
            return "";
        }

        var turret = GetTurret(req);
        RangedWeaponStats weaponStats = GetTurretStats(req);

        return DpsRangeBreakdown(weaponStats, turret);
    }
}