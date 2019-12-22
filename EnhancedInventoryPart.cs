using System.Linq;
using EnhancedEVA.Settings;

namespace EnhancedEVA
{
    // ReSharper disable once UnusedMember.Global
    public class EnhancedInventoryPart : ModuleInventoryPart
    {
        public override void OnStart(StartState state)
        {
            try
            {
                var settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedInventorySettings>();
                if (settings is null) return;

                isEnabled = InventoryEnabled(settings);
                InventorySlots = isEnabled ? settings.InventorySlots : 0;
            }
            finally
            {
                base.OnStart(state);
            }
        }

        private bool InventoryEnabled(EnhancedInventorySettings settings)
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