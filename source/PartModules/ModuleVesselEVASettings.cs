using System.Linq;
using EVASettings.GameSettings;

namespace EVASettings.PartModules
{
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public class ModuleVesselEVASettings : PartModule
    {
        // ReSharper disable once UnusedMember.Global
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            ApplyVisorSettings();
        }

        private void ApplyVisorSettings()
        {
            if (vessel is null)
            {
                return;
            }

            var miscSettings = HighLogic.CurrentGame.Parameters.CustomParams<EVAMiscSettings>();
            if (miscSettings is null || miscSettings.OverrideVisorState == false)
            {
                return;
            }

            foreach (var vesselCrewMember in vessel.GetVesselCrew())
            {
                if (HighLogic.fetch?.currentGame?.CrewRoster?.Crew?.FirstOrDefault(cm => cm.name == vesselCrewMember.name) is ProtoCrewMember rosterCrewMember)
                {
                    rosterCrewMember.hasVisorDown = miscSettings.HasVisorDown;
                }

                vesselCrewMember.hasVisorDown = miscSettings.HasVisorDown;
            }
        }
    }
}
