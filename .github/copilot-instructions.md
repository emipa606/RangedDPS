# GitHub Copilot Instructions for Ranged DPS (Continued) RimWorld Mod Project

## Mod Overview and Purpose

Ranged DPS (Continued) is a continuation of the original mod by Pausbraks, updated to enhance comparison of ranged weapons and turrets in the game RimWorld. By adding detailed statistical insights, the mod aims to help players make informed decisions about weapon and turret selections based on potential damage output.

## Key Features and Systems

- **Max Ranged DPS for Weapons and Turrets**: Shows the theoretical maximum damage per second (DPS) assuming perfect accuracy.
- **Average Ranged DPS for Weapons**: Provides a realistic expectation of DPS based on weapon accuracy.
- **Ranged DPS for Pawns**: Calculates the best DPS a pawn can achieve with their equipped weapon considering their shooting accuracy.
- **Turret Efficiency**: Calculates DPS considering inherent weapon and mount accuracy, as well as damage per resource spent.
- **Detailed Statistical Charts**: Clicking on any DPS stat provides a chart displaying DPS across various ranges.

## Coding Patterns and Conventions

- **Class Design**: The mod uses a set of specialized classes (e.g., `RangedWeaponStats`, `TurretStats`) extending from base classes such as `StatWorker_RangedDPSBase` to organize related functionality logically.
- **Inheritance**: Specialized `StatWorker` classes derive from shared base classes (e.g., `StatWorker_RangedDPSBase`) to maintain consistency and reuse common logic.
- **Method Accessibility**: Methods like those in `RangedWeaponStats` are either public for interfacing with other components or private for internal logic and calculations directly related to specific class functionality.

## XML Integration

- **XML-Definition Attributes**: Attributes and stats are defined in XML files to provide mod compatibility with RimWorld and other XML-centric mods.
- **Data-binding**: XML data definitions directly inform the calculation classes, aligning with RimWorld's data-driven design approach.
  
## Harmony Patching

- The mod does not specifically mention using Harmony for method patching; however, Harmony is a prevalent tool in the RimWorld modding community for modifying existing game methods. If needed, Harmony should be used to avoid directly altering core game code and maintain mod compatibility.
- Suggest creating an XML settings file to manage usability settings for Harmony patches if integrated later.

## Suggestions for Copilot

- **Enhanced Documentation**: Encourage Copilot to auto-generate comments for methods and classes to enhance code readability and maintainability.
- **Consistent Naming Conventions**: Suggest variable and method names that align with the naming conventions established in the project, particularly focusing on descriptive and specific names related to functionality.
- **Edge Case Handling**: Propose checks and balances within calculations, especially those handling pawn stats or turret efficiency to ensure robustness against unusual values or configurations.
- **Collaborative Contributions**: Copilot should suggest adding translation or compatibility notes in relevant code and encourage community contributions via pull requests, as translations are community-supported.
- **Test Case Recommendations**: Advocate for unit tests on DPS calculations to ensure accuracy across different mod combinations and ensure suggestions reflect this development need.

---

By providing structured guidelines and suggestions, this file serves to foster more effective and consistent development practices for contributors.
