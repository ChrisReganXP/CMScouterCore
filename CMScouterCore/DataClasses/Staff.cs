using System;

namespace CMScouterFunctions.DataClasses
{
    public class Staff
    {
        [DataFileInfo(0)]
        public int StaffId { get; set; }

        [DataFileInfo(97)]
        public int StaffPlayerId { get; set; }

        [DataFileInfo(4)]
        public int FirstNameId { get; set; }

        [DataFileInfo(8)]
        public int SecondNameId { get; set; }

        [DataFileInfo(12)]
        public int CommonNameId { get; set; }

        [DataFileInfo(16)]
        public DateTime DOB { get; set; }

        [DataFileInfo(26)]
        public int NationId { get; set; }

        [DataFileInfo(30)]
        public int SecondaryNationId { get; set; }

        [DataFileInfo(34)]
        public byte InternationalCaps { get; set; }

        [DataFileInfo(35)]
        public byte InternationalGoals { get; set; }

        [DataFileInfo(68)]
        public DateTime? ContractExpiryDate { get; set; }

        [DataFileInfo(78)]
        public int Wage { get; set; }

        [DataFileInfo(82)]
        public int Value { get; set; }

        [DataFileInfo(57)]
        public int ClubId { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Adaptability { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Ambition { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Determination { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Loyalty { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Pressure { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Professionalism { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Sportsmanship { get; set; }

        [AttributeGroup(AttributeGroup.OffField)]
        public byte Temperament { get; set; }

        public bool IsNationality(int nationId)
        {
            return NationId == nationId || SecondaryNationId == nationId;
        }

        public bool IsOverValue(int value)
        {
            return value <= Value;
        }

        public bool IsUnderValue(int value)
        {
            return value >= Value;
        }
    }

    public class DataFileInfoAttribute : Attribute
    {
        public int DataFilePosition;

        public bool IsIntrinsic;

        public int Length;

        public DataFileInfoAttribute(int position, int length = 0, bool isIntrinsic = false)
        {
            DataFilePosition = position;
            IsIntrinsic = isIntrinsic;
            Length = length;
        }
    }
}
