using System.Linq;
using System.Text;
using EnhancedEVA.Settings;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace EnhancedEVA
{
    public class EnhancedKerbalEVA : PartModule
    {
        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Kerbal")]
        public string KerbalTraitMenuLabel;
        
        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Experience")]
        public string KerbalExperienceMenuLabel;

        public BaseField KerbalTraitField { get; set; }
        public BaseField KerbalExperienceField { get; set; }

        public void Start()
        {
            KerbalTraitField = Fields[nameof(KerbalTraitMenuLabel)];
            KerbalExperienceField = Fields[nameof(KerbalExperienceMenuLabel)];
            ApplyActionWindowSettings();
            ApplyScienceSettings();
        }

        private void ApplyActionWindowSettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedEVAActionSettings>();
            if (!(settings is object)) return;
            if (!(vessel.GetVesselCrew().FirstOrDefault() is ProtoCrewMember kerbal)) return;

            if (settings.ShowTraitLabel && KerbalTraitField is object)
            {
                KerbalTraitMenuLabel = BuildInfo(kerbal);

                KerbalTraitField.guiActive = true;
                var sb = new StringBuilder();
                if (kerbal.isBadass)
                    sb.Append("Badass ");
                if (kerbal.veteran)
                    sb.Append($"{(kerbal.isBadass ? 'v' : 'V')}eteran ");

                sb.Append(kerbal.isBadass || kerbal.veteran ? kerbal.trait.ToLower() : kerbal.trait);
                KerbalTraitMenuLabel = sb.ToString();
            }

            if (settings.ShowExperienceLabel == false || KerbalExperienceField is null) return;
            KerbalExperienceField.guiActive = true;
            KerbalExperienceMenuLabel = kerbal.experienceLevel.ToString();
        }

        private void ApplyScienceSettings()
        {
            var settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedEVAScienceSettings>();
            if (settings.RemoveScience == false) return;
            part.FindModulesImplementing<ModuleScienceExperiment>().ForEach(e => part.RemoveModule(e));
            part.FindModulesImplementing<ModuleScienceContainer>().ForEach(e => part.RemoveModule(e));
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