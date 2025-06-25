using System;
using RimWorld;
using Verse;

namespace RangedDPS.StatUtilities;

public class TurretStats(Building_TurretGun turret) : RangedWeaponStats(turret)
{
    private readonly CompRefuelable compRefuelable = turret.TryGetComp<CompRefuelable>();
    public readonly Building_TurretGun Turret = turret;

    /// <summary>
    ///     Gets a value indicating whether this turret uses fuel (barrel refurbishing, ammo, etc.).
    /// </summary>
    /// <value><c>true</c> if it needs fuel; otherwise, <c>false</c>.</value>
    public bool NeedsFuel => compRefuelable != null;

    /// <summary>
    ///     Gets the amount of shots this turret can shoot per unit of fuel
    /// </summary>
    /// <value>The fuel per shot, or float.MaxValue if the turret does not use fuel.</value>
    private float ShotsPerFuel => !NeedsFuel ? float.MaxValue : compRefuelable.Props.FuelMultiplierCurrentDifficulty;

    /// <summary>
    ///     Gets the amount of fuel used per point of damage dealt by the turret (assuming the shot hits).
    ///     Returns 0 if the turret does not use fuel.
    /// </summary>
    /// <value>The fuel per damage, or float.MaxValue if the turret does not use fuel.</value>
    public float DamagePerFuel
    {
        get
        {
            if (!NeedsFuel)
            {
                return float.MaxValue;
            }

            return ShotsPerFuel * ShotDamage;
        }
    }

    /// <summary>
    ///     Gets the accuracy-adjusted damage this turret can do per unit of fuel at a particular range.
    /// </summary>
    /// <returns>The accuracy-adjusted ranged damage per fuel of the turret.</returns>
    /// <param name="range">The range of the shot.</param>
    public FloatRange GetAdjustedDamagePerFuel(float range)
    {
        return new FloatRange(DamagePerFuel * Math.Min(GetAdjustedHitChanceFactor(range, Turret), 1f),
            DamagePerFuel * Math.Min(GetAdjustedHitChanceFactor(range, Turret), 1f));
    }
}