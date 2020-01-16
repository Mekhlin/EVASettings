using System.Linq;
using EnhancedEVA.Settings;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace EnhancedEVA
{
    public class EnhancedKerbalEVA : PartModule
    {
        public void Start()
        {
            ApplyScienceSettings();
        }

        private void ApplyScienceSettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedEVAScienceSettings>();
            if (settings.RemoveScience)
            {
                part.FindModulesImplementing<ModuleScienceExperiment>().ForEach(e => part.RemoveModule(e));
                part.FindModulesImplementing<ModuleScienceContainer>().ForEach(e => part.RemoveModule(e));
                return;
            }

            if (!settings.RestrictSurfaceSample) return;
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