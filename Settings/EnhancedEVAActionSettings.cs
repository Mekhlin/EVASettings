namespace EnhancedEVA.Settings
{
    public class EnhancedEVAActionSettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Title => "Action Window";
        public override int SectionOrder => 1;
        public override string Section => SharedSettings.Section;
        public override string DisplaySection => SharedSettings.DisplaySection;
        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;
        public override bool HasPresets => SharedSettings.HasPresets;

        #endregion

        #region Settings

        [GameParameters.CustomParameterUI("Show trait", gameMode = GameParameters.GameMode.ANY)]
        public bool ShowTraitLabel = false;
        
        [GameParameters.CustomParameterUI("Show experience", gameMode = GameParameters.GameMode.ANY)]
        public bool ShowExperienceLabel = false;

        #endregion
    }
}
