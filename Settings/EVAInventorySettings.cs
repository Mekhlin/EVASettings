using System.Reflection;

namespace EVASettings.Settings
{
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

        [GameParameters.CustomIntParameterUI("Inventory slots", maxValue = 3, minValue = 1, stepSize = 1, toolTip = "Number of inventory slots available on EVA", autoPersistance = true)]
        public int InventorySlots = 1;

        [GameParameters.CustomParameterUI("Disable EVA inventory on pilots", toolTip = "Disable pilot inventory on EVA. Only scientists and engineers will have an inventory", autoPersistance = true, gameMode = GameParameters.GameMode.ANY)]
        public bool DisablePilotInventory = false;

        [GameParameters.CustomParameterUI("Disable EVA inventory", autoPersistance = true, gameMode = GameParameters.GameMode.ANY)]
        public bool DisableInventory = false;

        #endregion

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            switch (member.Name)
            {
                case nameof(DisableInventory):
                    return true;
                case nameof(InventorySlots):
                case nameof(DisablePilotInventory):
                    return DisableInventory == false;
                default:
                    return true;
            }
        }
    }
}
