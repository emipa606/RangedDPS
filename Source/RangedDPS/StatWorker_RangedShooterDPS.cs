﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_RangedShooterDPS : StatWorker_RangedDPSBase
{
    public override bool ShouldShowFor(StatRequest req)
    {
        if (base.ShouldShowFor(req))
        {
            return req.Thing is Pawn pawn && (GetPawnWeapon(pawn)?.def?.IsRangedWeapon ?? false);
        }

        return false;
    }

    public override bool IsDisabledFor(Thing thing)
    {
        return base.IsDisabledFor(thing) || StatDefOf.ShootingAccuracyPawn.Worker.IsDisabledFor(thing);
    }

    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
    {
        if (!ShouldShowFor(req))
        {
            return 0f;
        }

        var pawn = req.Thing as Pawn;
        var weaponStats = GetWeaponStats(GetPawnWeapon(pawn));

        var optimalRange = weaponStats.FindOptimalRange(pawn);
        return weaponStats.GetAdjustedDPS(optimalRange, pawn).Average;
    }

    public override string GetStatDrawEntryLabel(StatDef stat, float value, ToStringNumberSense numberSense,
        StatRequest optionalReq, bool finalized = true)
    {
        var pawn = optionalReq.Thing as Pawn;
        var weaponStats = GetWeaponStats(GetPawnWeapon(pawn));

        var optimalRange = (int)weaponStats.FindOptimalRange(pawn);

        return
            $"{value.ToStringByStyle(stat.toStringStyle, numberSense)} ({string.Format("StatsReport_RangedDPSOptimalRange".Translate(), optimalRange)})";
    }

    public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
    {
        if (!ShouldShowFor(req))
        {
            return "";
        }

        var pawn = req.Thing as Pawn;
        var weaponStats = GetWeaponStats(GetPawnWeapon(pawn));
        return DPSRangeBreakdown(weaponStats, pawn);
    }

    public override IEnumerable<Dialog_InfoCard.Hyperlink> GetInfoCardHyperlinks(StatRequest statRequest)
    {
        if (statRequest.Thing is Pawn { equipment.Primary: { } } pawn)
        {
            yield return new Dialog_InfoCard.Hyperlink(pawn.equipment.Primary);
        }
    }

    protected static Thing GetPawnWeapon(Pawn pawn)
    {
        return pawn?.equipment?.Primary;
    }
}