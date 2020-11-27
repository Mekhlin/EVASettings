using System.Linq;
using EVASettings.Settings;

namespace EVASettings
{
    // ReSharper disable once UnusedMember.Global
    public class ExtendedInventoryPart : ModuleInventoryPart
    {
        public override void OnStart(StartState state)
        {
            try
            {
                var settings = HighLogic.CurrentGame.Parameters.CustomParams<EVAInventorySettings>();
                if (settings is null) return;

                isEnabled = InventoryEnabled(settings);
                InventorySlots = isEnabled ? settings.InventorySlots : 0;
            }
            finally
            {
                base.OnStart(state);
            }
        }

        private bool InventoryEnabled(EVAInventorySettings settings)
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
