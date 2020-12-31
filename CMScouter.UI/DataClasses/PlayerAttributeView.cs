using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public class PlayerAttributeView
    {

        #region Physical

        public byte Acceleration { get; set; }

        public byte Agility { get; set; }

        public byte Balance { get; set; }

        public byte InjuryProneness { get; set; }

        public byte Jumping { get; set; }

        public byte NaturalFitness { get; set; }

        public byte Pace { get; set; }

        public byte Stamina { get; set; }

        public byte Strength { get; set; }

        #endregion

        #region Shared Technical

        public byte Anticipation { get; set; }

        public byte Decisions { get; set; }

        public byte Heading { get; set; }

        #endregion

        #region Attacking

        public byte Creativity { get; set; }

        public byte Crossing { get; set; }

        public byte Dribbling { get; set; }

        public byte Finishing { get; set; }

        public byte LongShots { get; set; }

        public byte OffTheBall { get; set; }

        public byte Passing { get; set; }

        public byte Technique { get; set; }

        #endregion

        #region Defending

        public byte Marking { get; set; }

        public byte Positioning { get; set; }

        public byte Tackling { get; set; }

        #endregion

        #region Mental

        public byte Aggression { get; set; }

        public byte Bravery { get; set; }

        public byte Consistency { get; set; }

        public byte Dirtiness { get; set; }

        public byte Flair { get; set; }

        public byte ImportantMatches { get; set; }

        public byte Influence { get; set; }

        public byte Teamwork { get; set; }

        public byte Versitility { get; set; }

        public byte WorkRate { get; set; }

        #endregion

        #region Set Pieces

        public byte Corners { get; set; }

        public byte FreeKicks { get; set; }

        public byte Penalties { get; set; }

        public byte ThrowIns { get; set; }

        #endregion

        #region GK

        public byte Handling { get; set; }

        public byte OneOnOnes { get; set; }

        public byte Reflexes { get; set; }

        #endregion

        public byte LeftFoot { get; set; }

        public byte RightFoot { get; set; }

        #region Personality

        public byte Adaptability { get; set; }

        public byte Ambition { get; set; }

        public byte Determination { get; set; }

        public byte Loyalty { get; set; }

        public byte Pressure { get; set; }

        public byte Professionalism { get; set; }

        public byte Sportsmanship { get; set; }

        public byte Temperament { get; set; }

        #endregion
    }
}
