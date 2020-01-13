using System.Linq;
using System.Text;
using EnhancedEVA.Settings;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace EnhancedEVA
{
    public class EnhancedKerbalEVA : PartModule
    {
        private ProtoCrewMember _kerbal;
        public ProtoCrewMember Kerbal => _kerbal ?? (_kerbal = vessel.GetVesselCrew().FirstOrDefault());


        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Kerbal")]
        public string KerbalTraitMenuLabel;
        
        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Experience")]
        public string KerbalExperienceMenuLabel;

        public BaseField KerbalTraitField { get; set; }
        public BaseField KerbalExperienceField { get; set; }

        public void Start()
        {
            if (Kerbal is null) return;
            KerbalTraitField = Fields[nameof(KerbalTraitMenuLabel)];
            KerbalExperienceField = Fields[nameof(KerbalExperienceMenuLabel)];
            ApplyActionWindowSettings();
            ApplyScienceSettings();
        }

        private void ApplyActionWindowSettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedEVAActionSettings>();
            if (!(settings is object)) return;

            if (settings.ShowTraitLabel && KerbalTraitField is object)
            {
                KerbalTraitMenuLabel = BuildInfo(Kerbal);
            }

            if (settings.ShowExperienceLabel == false || KerbalExperienceField is null) return;
            KerbalExperienceField.guiActive = true;
            KerbalExperienceMenuLabel = Kerbal.experienceLevel.ToString();
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

        private string BuildInfo(ProtoCrewMember kerbal)
        {
            var sb = new StringBuilder();
            if (kerbal.isBadass)
                sb.Append("Badass ");
            if (kerbal.veteran)
                sb.Append($"{(kerbal.isBadass ? 'v' : 'V')}eteran ");

            sb.Append(kerbal.isBadass || kerbal.veteran ? kerbal.trait.ToLower() : kerbal.trait);
            return sb.ToString();
        }
    }
}