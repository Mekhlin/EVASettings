using System.Linq;
using EVASettings.GameSettings;

// ReSharper disable once InconsistentNaming
namespace EVASettings.PartModules
{
    // ReSharper disable once UnusedMember.Global
    public class ModuleKerbalEVASettings : PartModule
    {
        // ReSharper disable once UnusedMember.Global
        public void Start()
        {
            ApplyInventorySettings();
            ApplyScienceSettings();
        }

        private void ApplyInventorySettings()
        {
            var inventorySettings = HighLogic.CurrentGame.Parameters.CustomParams<EVAInventorySettings>();
            var inventoryPart = part.FindModulesImplementing<ModuleInventoryPart>().FirstOrDefault();
            if (inventoryPart is object)
            {
                inventoryPart.InventorySlots = inventorySettings.InventorySlots;
            }
        }

        private void ApplyScienceSettings()
        {
            var scienceSettings = HighLogic.CurrentGame.Parameters.CustomParams<EVAScienceSettings>();
            if (scienceSettings.RemoveScience)
            {
                part.FindModulesImplementing<ModuleScienceExperiment>().ForEach(e => part.RemoveModule(e));
                part.FindModulesImplementing<ModuleScienceContainer>().ForEach(e => part.RemoveModule(e));
                return;
            }

            if (scienceSettings.RestrictSurfaceSample == false) return;
            if (!(vessel.GetVesselCrew().FirstOrDefault() is ProtoCrewMember kerbal)) return;
            if (kerbal.trait == KerbalRoster.scientistTrait) return;

            foreach (var scienceExperiment in part.FindModulesImplementing<ModuleScienceExperiment>())
            {
                if (scienceExperiment.experimentID != "surfaceSample") continue;
                part.RemoveModule(scienceExperiment);
            }
        }
    }
}
