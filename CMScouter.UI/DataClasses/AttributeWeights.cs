using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CMScouter.DataClasses;

namespace CMScouter.UI.DataClasses
{
    public class WeightCollection
    {
        public List<IndividualWeightSet> IndividualWeights { get; set; }

        public List<GroupedWeightSet> GroupedWeights { get; set; }

        public WeightCollection()
        {
            IndividualWeights = new List<IndividualWeightSet>();

            GroupedWeights = new List<GroupedWeightSet>();
        }
    }

    public class AttributeWeights
    {
        [DataMember(Name = "Determination", EmitDefaultValue = false)]
        public byte Determination { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public byte Anticipation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public byte Creativity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public byte Crossing { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public byte Decisions { get; set; }

        [DataMember(Name = "Dribbling", EmitDefaultValue = false)]
        public byte Dribbling { get; set; }

        [DataMember(EmitDefaultValue = false)]
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

        public byte Versatility { get; set; }

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

        public byte Loyalty { get; set; }

        public byte Pressure { get; set; }

        public byte Professionalism { get; set; }

        public byte Temperament { get; set; }
    }

    public class IndividualRoleWeights : AttributeWeights
    {
        public Roles Role { get; set; }

        public IndividualRoleWeights()
        {
            Role = Roles.GK;
        }

        public IndividualRoleWeights(Roles role)
        {
            Role = role;
        }
    }

    public class IndividualWeightSet
    {
        public string Name { get; set; }

        public Guid ID { get; set; }

        public AttributeWeights GKWeights { get; set; }

        public AttributeWeights RBWeights { get; set; }

        public AttributeWeights CBWeights { get; set; }

        public AttributeWeights LBWeights { get; set; }

        public AttributeWeights DMWeights { get; set; }

        public AttributeWeights WBWeights { get; set; }

        public AttributeWeights RMWeights { get; set; }

        public AttributeWeights CMWeights { get; set; }

        public AttributeWeights LMWeights { get; set; }

        public AttributeWeights RWWeights { get; set; }

        public AttributeWeights AMWeights { get; set; }

        public AttributeWeights LWWeights { get; set; }

        public AttributeWeights CFWeights { get; set; }

        public IndividualWeightSet()
        {
            Name = "unknown";
        }

        public IndividualWeightSet(string name)
        {
            Name = name;
        }
    }

    public class GroupedRoleWeights
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

        public GroupedRoleWeights()
        {
            Role = Roles.GK;
        }

        public GroupedRoleWeights(Roles role)
        {
            Role = role;
        }
    }

    public class GroupedWeightSet
    {
        public string Name { get; set; }

        public Guid ID { get; set; }

        public AttributeWeights SpeedWeights { get; set; }

        public AttributeWeights StrengthWeights { get; set; }

        public AttributeWeights PlaymakingWeights { get; set; }

        public AttributeWeights ScoringWeights { get; set; }

        public AttributeWeights DefendingWeights { get; set; }

        public AttributeWeights WideplayWeights { get; set; }

        public AttributeWeights ImpactWeights { get; set; }

        public AttributeWeights ReliabilityWeights { get; set; }

        public AttributeWeights GoalkeepingWeights { get; set; }

        public List<GroupedRoleWeights> RoleWeights { get; set; }

        public GroupedWeightSet()
        {
            Name = "unknown";
            RoleWeights = new List<GroupedRoleWeights>();
        }

        public GroupedWeightSet(string name)
        {
            Name = name;
            RoleWeights = new List<GroupedRoleWeights>();
        }
    }
}
