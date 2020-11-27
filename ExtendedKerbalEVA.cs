using System.Linq;
using EVASettings.Settings;

// ReSharper disable once InconsistentNaming
namespace EVASettings
{
    // ReSharper disable once UnusedMember.Global
    public class ExtendedKerbalEVA : PartModule
    {
        // ReSharper disable once UnusedMember.Global
        public void Start()
        {
            ApplyScienceSettings();
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
