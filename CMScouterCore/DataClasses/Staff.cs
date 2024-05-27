using CMScouterCore.DataClasses;
using System;
using System.Collections.Generic;

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

        public Retirement Retirement { get; set; }

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

        public List<string> GetNameVarients(Dictionary<int, string> firstNames, Dictionary<int, string> surnames, Dictionary<int, string> commonNames)
        {
            List<string> names = new List<string>();
            string firstName = firstNames[FirstNameId].Replace(" ", "");
            string secondName = surnames[SecondNameId].Replace(" ", "");
            string commonName = commonNames[CommonNameId].Replace(" ", "");

            names.Add(firstName + "" + secondName);
            names.Add(secondName + "," + firstName);
            names.Add(commonName);

            return names;
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
