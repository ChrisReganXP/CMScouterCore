using System;

namespace CMScouterFunctions.DataClasses
{
    public enum AttributeGroup
    {
        Mental,

        Physical,

        Technical,

        OffField,
    }

    public class AttributeGroupAttribute : Attribute
    {
        public AttributeGroup Grouping { get; set; }

        public AttributeGroupAttribute(AttributeGroup grouping)
        {
            Grouping = grouping;
        }
    }

    public class Player
    {
        public PlayerData _player;

        public Staff _staff;

        public Contract _contract;

        public Player(PlayerData playerData, Staff staffData, Contract contract)
        {
            _player = playerData;
            _staff = staffData;
            _contract = contract;
        }
    }


    public class PlayerData
    {
        [DataFileInfo(0)]
        public int PlayerId { get; set; }

        [DataFileInfo(5)]
        public short CurrentAbility { get; set; }

        [DataFileInfo(7)]
        public short PotentialAbility { get; set; }

        [DataFileInfo(9)]
        public short Reputation { get; set; }

        [DataFileInfo(11)]
        public short DomesticReputation { get; set; }

        [DataFileInfo(13)]
        public short WorldReputation { get; set; }

        [DataFileInfo(15)]
        public byte GK { get; set; }

        [DataFileInfo(16)]
        public byte SW { get; set; }

        [DataFileInfo(17)]
        public byte DF { get; set; }

        [DataFileInfo(18)]
        public byte DM { get; set; }

        [DataFileInfo(19)]
        public byte MF { get; set; }

        [DataFileInfo(20)]
        public byte AM { get; set; }

        [DataFileInfo(21)]
        public byte ST { get; set; }

        [DataFileInfo(22)]
        public byte WingBack { get; set; }

        [DataFileInfo(24)]
        public byte Left { get; set; }

        [DataFileInfo(23)]
        public byte Right { get; set; }

        [DataFileInfo(25)]
        public byte Centre { get; set; }

        [DataFileInfo(26)]
        public byte FreeRole { get; set; }

        [DataFileInfo(48)]
        public byte LeftFoot { get; set; }

        [DataFileInfo(59)]
        public byte RightFoot { get; set; }


        [DataFileInfo(27, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Acceleration { get; set; }

        [DataFileInfo(29, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Agility { get; set; }

        [DataFileInfo(31, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Balance { get; set; }

        [DataFileInfo(46, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Jumping { get; set; }

        [DataFileInfo(54, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Pace { get; set; }

        [DataFileInfo(60, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Stamina { get; set; }

        [DataFileInfo(61, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Physical)]
        public byte Strength { get; set; }



        [DataFileInfo(28, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Aggression { get; set; }

        [DataFileInfo(32, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Bravery { get; set; }

        [DataFileInfo(33, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Consistency { get; set; }

        [DataFileInfo(40, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Flair { get; set; }

        [DataFileInfo(44, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte ImportantMatches { get; set; }

        [DataFileInfo(47, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Influence { get; set; }

        [DataFileInfo(63, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte Teamwork { get; set; }

        [DataFileInfo(68, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Mental)]
        public byte WorkRate { get; set; }



        [DataFileInfo(30, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Anticipation { get; set; }

        [DataFileInfo(67, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Creativity { get; set; }

        [DataFileInfo(35, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Crossing { get; set; }

        [DataFileInfo(36, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Decisions { get; set; }

        [DataFileInfo(38, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Dribbling { get; set; }

        [DataFileInfo(39, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Finishing { get; set; }

        [DataFileInfo(42, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Handling { get; set; }

        [DataFileInfo(43, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Heading { get; set; }

        [DataFileInfo(49, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte LongShots { get; set; }

        [DataFileInfo(50, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Marking { get; set; }

        [DataFileInfo(51, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte OffTheBall { get; set; }

        [DataFileInfo(53, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte OneOnOnes { get; set; }

        [DataFileInfo(55, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Passing { get; set; }

        [DataFileInfo(57, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Positioning { get; set; }

        [DataFileInfo(58, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Reflexes { get; set; }

        [DataFileInfo(62, IsIntrinsic = true)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Tackling { get; set; }

        [DataFileInfo(64, IsIntrinsic = false)]
        [AttributeGroup(AttributeGroup.Technical)]
        public byte Technique { get; set; }


        [DataFileInfo(34, IsIntrinsic = false)]
        public byte Corners { get; set; }

        [DataFileInfo(37, IsIntrinsic = false)]
        public byte Dirtiness { get; set; }

        [DataFileInfo(41, IsIntrinsic = false)]
        public byte FreeKicks { get; set; }

        [DataFileInfo(45, IsIntrinsic = false)]
        public byte InjuryProneness { get; set; }

        [DataFileInfo(52, IsIntrinsic = false)]
        public byte NaturalFitness { get; set; }

        [DataFileInfo(56, IsIntrinsic = true)]
        public byte Penalties { get; set; }

        [DataFileInfo(65, IsIntrinsic = true)]
        public byte ThrowIns { get; set; }

        [DataFileInfo(66, IsIntrinsic = false)]
        public byte Versatility { get; set; }
    }
}
