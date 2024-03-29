﻿using System.Reflection;
using EVASettings.Settings;

namespace EVASettings.GameSettings
{
    // ReSharper disable once InconsistentNaming
    public class EVAScienceSettings : GameParameters.CustomParameterNode
    {
        #region Housekeeping

        public override string Title => "EVA Science";
        public override int SectionOrder => 1;
        public override string Section => SharedSettings.Section;
        public override string DisplaySection => SharedSettings.DisplaySection;
        public override GameParameters.GameMode GameMode => SharedSettings.GameMode;
        public override bool HasPresets => SharedSettings.HasPresets;

        #endregion

        #region Settings

        [GameParameters.CustomParameterUI("Remove all EVA science experiments", gameMode = GameParameters.GameMode.ANY)]
        public bool RemoveScience = false;
        
        [GameParameters.CustomParameterUI("Only scientists can take surface samples", gameMode = GameParameters.GameMode.ANY)]
        public bool RestrictSurfaceSample = false;

        #endregion

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return member.Name == nameof(RemoveScience) || RemoveScience == false;
        }
    }
}
