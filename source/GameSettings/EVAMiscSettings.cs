using System.Reflection;
using EVASettings.Settings;

namespace EVASettings.GameSettings
{
    // ReSharper disable once InconsistentNaming
    public class EVAMiscSettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Title => "Miscellaneous";
        public override int SectionOrder => 2;
        public override string Section => SharedSettings.Section;
        public override string DisplaySection => SharedSettings.DisplaySection;
        public override GameParameters.GameMode GameMode => SharedSettings.GameMode;
        public override bool HasPresets => SharedSettings.HasPresets;

        #endregion

        #region Settings

        /// <summary>
        /// Overrides the current visor state of the Kerbal.
        /// </summary>
        [GameParameters.CustomParameterUI("Override visor state", toolTip = "Setting is only triggered if vessel is reloaded", gameMode = GameParameters.GameMode.ANY)]
        public bool OverrideVisorState = false;
        
        /// <summary>
        /// Is visor is lowered when exiting vessel.
        /// </summary>
        [GameParameters.CustomParameterUI("Lower visor on EVA", toolTip = "Visor is lowered when exiting vessel. Setting is only triggered if vessel is reloaded", gameMode = GameParameters.GameMode.ANY)]
        public bool HasVisorDown = false;

        #endregion

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return member.Name != nameof(HasVisorDown) || OverrideVisorState;
        }
    }
}
