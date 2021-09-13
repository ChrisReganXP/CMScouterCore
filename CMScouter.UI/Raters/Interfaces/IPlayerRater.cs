using CMScouter.UI.Raters;
using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;
using CMScouter.DataContracts;

namespace CMScouter.UI
{
    public interface IPlayerRater
    {
        void OutputDebug(bool enabled);

        ScoutingInformation GetRatings(Player item);

        bool PlaysPosition(PlayerPosition type, PlayerData player);

        ScoutingInformation GetRating(Player item, ConstructPlayerOptions options);
    }
}
