using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMScouter.DataClasses;

namespace CMScouter.UI.DataClasses
{
    public class AttributeWeights
    {
        public byte Anticipation { get; set; }

        public byte Creativity { get; set; }

        public byte Crossing { get; set; }

        public byte Decisions { get; set; }

        public byte Dribbling { get; set; }

        public byte Finishing { get; set; }

        public byte Heading { get; set; }

        public byte LongShots { get; set; }

        public byte Marking { get; set; }

        public byte OffTheBall { get; set; }

        public byte Passing { get; set; }

        public byte Positioning { get; set; }

        public byte Tackling { get; set; }

        public byte Technique { get; set; }

        public byte Aggression { get; set; }

        public byte Bravery { get; set; }

        public byte Consistency { get; set; }

        public byte Flair { get; set; }

        public byte ImportantMatches { get; set; }

        public byte Influence { get; set; }

        public byte Teamwork { get; set; }

        public byte Versitility { get; set; }

        public byte WorkRate { get; set; }

        public byte Acceleration { get; set; }

        public byte Agility { get; set; }

        public byte Balance { get; set; }

        public byte Jumping { get; set; }

        public byte NaturalFitness { get; set; }

        public byte Pace { get; set; }

        public byte Stamina { get; set; }

        public byte Strength { get; set; }

        public byte Handling { get; set; }

        public byte OneonOnes { get; set; }

        public byte Reflexes { get; set; }
    }

    public class GroupedRoleWeights : AttributeWeights
    {
        public byte SpeedPercent { get; set; }

        public byte StrengthPercent { get; set; }

        public byte PlaymakingPercent { get; set; }

        public byte ScoringPercent { get; set; }

        public byte DefendingPercent { get; set; }

        public byte WideplayPercent { get; set; }

        public byte ImpactPercent { get; set; }

        public byte ReliabilityPercent { get; set; }

        public byte GoalkeepingPercent { get; set; }

        public Roles Role { get; set; }

        public GroupedRoleWeights(Roles role)
        {
            Role = role;
        }
    }
}
