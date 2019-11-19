using System.Reflection;

namespace EnhancedEVA.Settings
{
    public class EnhancedInventorySettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Section => "EnhancedEVA";
        public override string DisplaySection => "Enhanced EVA";
        public override string Title => "EVA inventory";
        public override int SectionOrder => 0;
        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;
        public override bool HasPresets => false;

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
