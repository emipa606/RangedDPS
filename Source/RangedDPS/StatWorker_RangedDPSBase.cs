using System;
using System.Text;
using RangedDPS.StatUtilities;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_RangedDPSBase : StatWorker
{
    protected static RangedWeaponStats GetWeaponStats(StatRequest req)
    {
        var weapon = req.Thing ?? (req.Def as ThingDef)?.GetConcreteExample();
        if (weapon == null)
        {
            Log.Error(
                $"[RangedDPS] Could not find a valid weapon thing when trying to caluculate the stat for {req.Def.defName}");
        }

        return GetWeaponStats(weapon);
    }

    /// <summary>
    ///     Calculates a stats breakdown of the given ranged weapon.
    ///     Logs an error and returns null if the thing is null or not a ranged weapon.
    /// </summary>
    /// <returns>The stats of the passed-in weapon.</returns>
    /// <param name="weapon">The turret to get stats for.</param>
    protected static RangedWeaponStats GetWeaponStats(Thing weapon)
    {
        if (weapon == null)
        {
            Log.Error("[RangedDPS] Tried to get the ranged weapon stats of a null weapon");
            return null;
        }

        if (weapon.def.IsRangedWeapon)
        {
            return new RangedWeaponStats(weapon);
        }

        Log.Error(
            $"[RangedDPS] Tried to get the ranged weapon stats of {weapon.def.defName}, which is not a ranged weapon");
        return null;
    }

    /// <summary>
    ///     Returns a string that provides a breakdown of both accuracy and DPS over the full range of the given weapon.
    ///     Shooter is optional, and if provided the DPS is adjusted to account for the shooter's shooting accuracy.
    /// </summary>
    /// <returns>A string providing a breakdown of the performance of the given weapon at various ranges.</returns>
    /// <param name="gun">The gun to caluclate a breakdown for.</param>
    /// <param name="shooter">(Optional) The shooter (pawn or turret) using the weapon.</param>
    protected static string DpsRangeBreakdown(RangedWeaponStats gun, Thing shooter = null)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("StatsReport_RangedDPSAccuracy".Translate());

        // Min Range
        var minRange = Math.Max(gun.MinRange, 1f);
        var minRangeHitChance = gun.GetAdjustedHitChanceFactor(minRange, shooter);
        var minRangeDps = gun.GetAdjustedDps(minRange, shooter);
        stringBuilder.AppendLine(FormatValueRangeString(minRange, minRangeDps, minRangeHitChance));

        // Ranges between Min - Max, in steps of 5
        var startRange = (float)Math.Ceiling(minRange / 5) * 5;
        for (var range = startRange; range < gun.MaxRange; range += 5)
        {
            var hitChance = gun.GetAdjustedHitChanceFactor(range, shooter);
            var dps = gun.GetAdjustedDps(range, shooter);
            stringBuilder.AppendLine(FormatValueRangeString(range, dps, hitChance));
        }

        // Max Range
        var maxRangeHitChance = gun.GetAdjustedHitChanceFactor(gun.MaxRange, shooter);
        var maxRangeDps = gun.GetAdjustedDps(gun.MaxRange, shooter);
        stringBuilder.AppendLine(FormatValueRangeString(gun.MaxRange, maxRangeDps, maxRangeHitChance));

        return stringBuilder.ToString();
    }

    protected static string FormatValueRangeString(float range, FloatRange value, float hitChance)
    {
        return value.min == value.max
            ? $"{"distance".Translate().CapitalizeFirst()} {range,2}: {value.Average,5:F2} ({hitChance:P1})"
            : $"{"distance".Translate().CapitalizeFirst()} {range,2}: {value.min,5:F2}-{value.max,5:F2} ({hitChance:P1})";
    }
}