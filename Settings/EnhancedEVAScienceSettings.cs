namespace EnhancedEVA.Settings
{
    public class EnhancedEVAScienceSettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Title => "EVA Science";
        public override int SectionOrder => 2;
        public override string Section => SharedSettings.Section;
        public override string DisplaySection => SharedSettings.DisplaySection;
        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;
        public override bool HasPresets => SharedSettings.HasPresets;

        #endregion

        #region Settings

        [GameParameters.CustomParameterUI("Remove all science experiments", gameMode = GameParameters.GameMode.ANY)]
        public bool RemoveScience = false;

        #endregion
    }
}
