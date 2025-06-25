using RimWorld;

namespace RangedDPS;

public class StatWorker_TurretMaxDPS : StatWorker_TurretDPSBase
{
    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
    {
        return !ShouldShowFor(req) ? 0f : GetTurretStats(req).GetRawDps().Average;
    }
}