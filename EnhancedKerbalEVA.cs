using System.Linq;
using System.Text;
using EnhancedEVA.Settings;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace EnhancedEVA
{
    public class EnhancedKerbalEVA : PartModule
    {
        private EnhancedEVAActionSettings _settings;

        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Kerbal")]
        public string KerbalTraitMenuLabel;
        
        [KSPField(guiActive = false, guiActiveEditor = false, guiName = "Experience")]
        public string KerbalExperienceMenuLabel;

        [KSPEvent(guiActive = false, guiActiveEditor = false, guiName = "Toggle info")]
        public void ToggleInfo()
        {
            if (!(_settings is object)) return;
            if (KerbalTraitField is object)
                KerbalTraitField.guiActive = _settings.ShowTraitLabel && KerbalTraitField.guiActive == false;
            if (KerbalExperienceField is object)
                KerbalExperienceField.guiActive = _settings.ShowExperienceLabel && KerbalExperienceField.guiActive == false;
        }

        public BaseField KerbalTraitField { get; set; }
        public BaseField KerbalExperienceField { get; set; }
        public BaseEvent TogleInfoEvent { get; set; }

        public void Start()
        {
            _settings = HighLogic.CurrentGame.Parameters.CustomParams<EnhancedEVAActionSettings>();
            if (!(_settings is object)) return;
            KerbalTraitField = Fields[nameof(KerbalTraitMenuLabel)];
            KerbalExperienceField = Fields[nameof(KerbalExperienceMenuLabel)];
            TogleInfoEvent = Events[nameof(ToggleInfo)];

            KerbalTraitField.guiActive = _settings.ShowTraitLabel;
            KerbalExperienceField.guiActive = _settings.ShowExperienceLabel;
            TogleInfoEvent.guiActive = _settings.ShowToggleButton;

            if (!(vessel.GetVesselCrew().FirstOrDefault() is ProtoCrewMember kerbal)) return;
            KerbalTraitMenuLabel = BuildInfo(kerbal);
            KerbalExperienceMenuLabel = kerbal.experienceLevel.ToString();
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