using EVASettings.Settings;

namespace EVASettings.GameSettings
{
    // ReSharper disable once InconsistentNaming
    public class EVAInventorySettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Title => "EVA Inventory";
        public override int SectionOrder => 0;
        public override string Section => SharedSettings.Section;
        public override string DisplaySection => SharedSettings.DisplaySection;
        public override GameParameters.GameMode GameMode => SharedSettings.GameMode;
        public override bool HasPresets => SharedSettings.HasPresets;

        #endregion

        #region Settings

        /// <summary>
        /// Number of inventory slots available on EVA.
        /// </summary>
        [GameParameters.CustomIntParameterUI("Inventory slots", minValue = 2, maxValue = 3, stepSize = 1, toolTip = "Number of inventory slots available on EVA", autoPersistance = true)] 
        public int InventorySlots = 2;

        #endregion
    }
}
