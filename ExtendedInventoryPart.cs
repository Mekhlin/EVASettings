using System.Linq;
using ExtendedEVA.Settings;

namespace ExtendedEVA
{
    // ReSharper disable once UnusedMember.Global
    public class ExtendedInventoryPart : ModuleInventoryPart
    {
        public override void OnStart(StartState state)
        {
            try
            {
                var settings = HighLogic.CurrentGame.Parameters.CustomParams<ExtendedInventorySettings>();
                if (settings is null) return;

                isEnabled = InventoryEnabled(settings);
                InventorySlots = isEnabled ? settings.InventorySlots : 0;
            }
            finally
            {
                base.OnStart(state);
            }
        }

        private bool InventoryEnabled(ExtendedInventorySettings settings)
        {
            try
            {
                if (!(settings is object) || settings.DisableInventory) return false;
                if (!(vessel.GetVesselCrew().FirstOrDefault() is ProtoCrewMember kerbal)) return false;
                return kerbal is object && (settings.DisablePilotInventory && kerbal.trait == KerbalRoster.pilotTrait) == false;
            }
            catch
            {
                return false;
            }
        }
    }
}
