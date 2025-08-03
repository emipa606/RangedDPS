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

        var weapon = GetTurretWeapon(GetTurret(req));
        if (weapon == null)
        {
            return false;
        }

        // Don't show DPS for unloaded mortars
        var comp = weapon.TryGetComp<CompChangeableProjectile>();
        return comp == null || comp.Loaded;
    }

    protected static Building_Turret GetTurret(StatRequest req)
    {
        if (req.Thing is Building_Turret turret)
        {
            return turret;
        }

        return (req.Def as ThingDef)?.GetConcreteExample() as Building_Turret;
    }

    private static Thing GetTurretWeapon(Building_Turret turret)
    {
        if (turret == null)
        {
            return null;
        }

        // Fast-path for vanilla Building_TurretGun, which is the most common case.
        // It uses a public field, not a property.
        if (turret is Building_TurretGun turretGun)
        {
            return turretGun.gun;
        }

        // Fallback to reflection for mod compatibility.
        // The original code used GetProperty, but vanilla uses a field. We check for the field first,
        // then for a property as some mods might implement it that way.
        return turret.GetType().GetField("gun")?.GetValue(turret) as Thing ??
            turret.GetType().GetProperty("gun")?.GetValue(turret) as Thing;
    }

    /// <summary>
    ///     Calculates a stats breakdown of the given turret.
    ///     Logs an error and returns null if the thing is null.
    /// </summary>
    /// <returns>The stats of the passed-in turret.</returns>
    protected static TurretStats GetTurretStats(StatRequest req)
    {
        var turret = GetTurret(req);
        if (turret != null)
        {
            var weapon = GetTurretWeapon(turret);
            if (weapon != null)
            {
                return new TurretStats(turret, weapon);
            }
        }

        Log.Error("[RangedDPS] Tried to get the ranged weapon stats of a null turret");
        return null;
    }
}