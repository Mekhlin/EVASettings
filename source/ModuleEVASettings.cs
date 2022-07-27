using System.Linq;
using EVASettings.GameSettings;

// ReSharper disable once InconsistentNaming
namespace EVASettings
{
    // ReSharper disable once UnusedMember.Global
    public class ModuleEVASettings : PartModule
    {
        // ReSharper disable once UnusedMember.Global
        public void Start()
        {
            ApplyInventorySettings();
            ApplyScienceSettings();
        }

        private void ApplyInventorySettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EVAInventorySettings>();
            var inventoryPart = part.FindModulesImplementing<ModuleInventoryPart>().FirstOrDefault();
            if (inventoryPart is object)
            {
                inventoryPart.InventorySlots = settings.InventorySlots;
            }
        }

        private void ApplyScienceSettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EVAScienceSettings>();
            if (settings.RemoveScience)
            {
                part.FindModulesImplementing<ModuleScienceExperiment>().ForEach(e => part.RemoveModule(e));
                part.FindModulesImplementing<ModuleScienceContainer>().ForEach(e => part.RemoveModule(e));
                return;
            }

            if (settings.RestrictSurfaceSample == false) return;
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
