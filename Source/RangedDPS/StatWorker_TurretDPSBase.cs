using RangedDPS.StatUtilities;
using RimWorld;
using Verse;

namespace RangedDPS;

public class StatWorker_TurretDPSBase : StatWorker_RangedDPSBase
{
    public override bool ShouldShowFor(StatRequest req)
    {
        if (!(req.Def is ThingDef thingDef && (thingDef.building?.turretGunDef?.IsRangedWeapon ?? false)))
        {
            return false;
        }

        // Don't show DPS for unloaded mortars
        var comp = getTurretWeapon(req).TryGetComp<CompChangeableProjectile>();
        return comp == null || comp.Loaded;
    }

    protected static Building_TurretGun GetTurret(StatRequest req)
    {
        if (req.Thing is Building_TurretGun turret)
        {
            return turret;
        }

        return (req.Def as ThingDef)?.GetConcreteExample() as Building_TurretGun;
    }

    private static Thing getTurretWeapon(StatRequest req)
    {
        return GetTurret(req).gun;
    }

    protected static TurretStats GetTurretStats(StatRequest req)
    {
        return getTurretStats(GetTurret(req));
    }

    /// <summary>
    ///     Calculates a stats breakdown of the given turret.
    ///     Logs an error and returns null if the thing is null.
    /// </summary>
    /// <returns>The stats of the passed-in turret.</returns>
    /// <param name="turret">The turret to get stats for.</param>
    private static TurretStats getTurretStats(Building_TurretGun turret)
    {
        if (turret != null)
        {
            return new TurretStats(turret);
        }

        Log.Error("[RangedDPS] Tried to get the ranged weapon stats of a null turret");
        return null;
    }
}