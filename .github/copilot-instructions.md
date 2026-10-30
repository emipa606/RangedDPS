# GitHub Copilot Instructions for Ranged DPS (Continued) Mod

## Mod Overview and Purpose

Ranged DPS (Continued) is an update of Pausbrak's mod. It provides an easy way to compare ranged weapons and turrets by adding various stats to weapons, pawns, and turrets. These stats include Max Ranged DPS, Average Ranged DPS, Ranged DPS, Weapon Ranged DPS, Turret Max Ranged DPS, and Damage per Resource.

## Key Features and Systems

### Added Stats

#### Weapons
- **Max Ranged DPS:** Represents the maximum possible DPS a weapon could achieve if every bullet hit.
- **Average Ranged DPS:** Represents the best possible DPS a pawn could realistically achieve with this weapon, based only on the inherent accuracy of the weapon.
- **Optimal Range:** Clicking on the optimal range provides a detailed chart covering the full range of the weapon.

#### Pawns
- **Ranged DPS:** Represents the best DPS this specific pawn can achieve with their currently equipped weapon, based on their current shooting accuracy.
- **Optimal Range:** Clicking on the optimal range provides a detailed chart covering the full range of the weapon.

#### Turrets
- **Max Ranged DPS:** Represents the maximum possible DPS the turret could achieve if every bullet hit.
- **Weapon Ranged DPS:** Represents the stats of the built-in weapon of the turret.
- **Ranged DPS:** Represents the DPS this turret can achieve, factoring in both the inherent accuracy of the turret's weapon and the shooting accuracy of the turret mount.
- **Damage per Resource:** Measures the efficiency of the turret at turning resources into damage.
- **Max Damage per Resource:** Represents the maximum possible damage per unit of resource the turret could achieve if every bullet hit.
- **Optimal Range:** Clicking on the optimal range provides a detailed chart covering the full range of the turret.

### Compatibility
- Compatible with all XML-only weapon mods.
- Not compatible with Combat Extended™.

## Coding Patterns and Conventions

- Use meaningful and descriptive names for classes, methods, and variables.
- Follow the RimWorld coding conventions, such as naming conventions and structure.
- Use the `DefOf` pattern for accessing `Def` objects.
- Utilize the `StatWorker` class for defining new stats.
- Use `Harmony` for patching existing RimWorld code.

## XML Integration

- Define new `StatDef` objects in XML files (e.g., `Stats_RangedDPS_Guns.xml` and `Stats_RangedDPS_Turrets.xml`).
- Ensure XML files are correctly referenced in the `Manifest.xml` and `About.xml` files.

## Harmony Patching

- Use the `Harmony` library to patch existing RimWorld code.
- Example patch to add a new stat to a weapon:

csharp
using HarmonyLib;
using RimWorld;
using Verse;

namespace RangedDPS
{
    [HarmonyPatch(typeof(ThingDef), "GetStatValueAbstract")]
    public static class Patch_GetStatValueAbstract
    {
        [HarmonyPostfix]
        public static void Postfix(StatDef stat, ref float __result)
        {
            if (stat == StatDefOf.MaxRangedDPS)
            {
                __result = CalculateMaxRangedDPS();
            }
            else if (stat == StatDefOf.AverageRangedDPS)
            {
                __result = CalculateAverageRangedDPS();
            }
        }

        private static float CalculateMaxRangedDPS()
        {
            // Implementation for calculating max ranged DPS
            return 0f;
        }

        private static float CalculateAverageRangedDPS()
        {
            // Implementation for calculating average ranged DPS
            return 0f;
        }
    }
}


## Suggestions for Copilot

1. **Generate StatWorkers for New Stats:**
   - Provide examples of `StatWorker` classes for new stats like `StatWorker_TurretMaxDamagePerResource` and `StatWorker_TurretMaxDPS`.

2. **Patch Existing Methods:**
   - Suggest patches for methods that need to be modified to include new stats or functionality.

3. **XML File Examples:**
   - Provide examples of XML files defining new `StatDef` objects.

4. **General Modding Tips:**
   - Offer tips for modding in RimWorld, such as using `DefOf` patterns and structuring code correctly.

5. **Debugging and Testing:**
   - Provide guidance on debugging and testing mod code to ensure compatibility and accuracy.

By following these instructions and suggestions, developers can effectively contribute to the Ranged DPS (Continued) mod and enhance the modding experience in RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

