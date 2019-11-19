namespace EnhancedEVA.Settings
{
    public class EnhancedEVAActionSettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Section => "EnhancedEVA";
        public override string DisplaySection => "Enhanced EVA";
        public override string Title => "Action Window";
        public override int SectionOrder => 1;
        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;
        public override bool HasPresets => false;

        #endregion

        #region Settings

        [GameParameters.CustomParameterUI("Show trait", gameMode = GameParameters.GameMode.ANY)]
        public bool ShowTraitLabel = false;
        
        [GameParameters.CustomParameterUI("Show experience", gameMode = GameParameters.GameMode.ANY)]
        public bool ShowExperienceLabel = false;
        
        [GameParameters.CustomParameterUI("Show toggle button", gameMode = GameParameters.GameMode.ANY)]
        public bool ShowToggleButton = false;

        #endregion
    }
}
